using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingCL
{
    public sealed class Parking : IParking
    {
        private List<Car> cars = new List<Car>();

        private List<Transaction> transactions = new List<Transaction>();

        private object transactionsSyncRoot = new object();

        private decimal parkingBalance;

        private Timer calcTimer;
        private Timer logTimer;

        public Parking()
        {
            this.calcTimer = new Timer(new TimerCallback(PayCalc), null, Settings.Timeout, Settings.Timeout);
            this.logTimer = new Timer(new TimerCallback(WriteLogAndCleanTransactions), null, Settings.LogTimeout, Settings.LogTimeout);
        }

        private IList<T> CloneList<T>(IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Adds a unique car to the parking.
        /// </summary>
        /// <param name="car"></param>
        /// <returns>false if car == null, parking is full or car is not unique</returns>
        public Error AddCar(Car car)
        {
            if (car == null) return Error.IsNullOrWhiteSpace;

            if (cars.Count >= Settings.ParkingSpace) return Error.ParkingIsFull;

            if (cars.Count(x => x.Id == car.Id) > 0) return Error.AlreadyExists;

            cars.Add(car);
            return Error.Success;
        }

        /// <summary>
        /// Removing car from parking
        /// </summary>
        /// <param name="id">Car Id</param>
        /// <returns> 1 - car successfully deleted; 0 - car not deleted; -1 - carLicensePlate IsNullOrWhiteSpace; -2 - ar not found; -3 - The Car has a negative balance</returns>
        public Error DelCar(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return Error.IsNullOrWhiteSpace;

            Car delCar = cars.FirstOrDefault<Car>(x => x.Id == id);
            if (delCar == null) return Error.NotFound;

            if (delCar.Balance < 0) return Error.NegativeBalance;

            if (cars.Remove(delCar))
            {
                return Error.Success;
            }
            else
            {
                return Error.Error;
            }
        }

        public Task<Car> GetCarByIdAsync(string id)
            => Task.Run(()
                =>{
                    if (String.IsNullOrWhiteSpace(id)) return null;
                    return cars.FirstOrDefault<Car>(x => x.Id == id);
                });


        private void PayCalc(object o)
        {
            foreach (Car car in cars)
            {
                decimal parkingPrice = Settings.ParkingPrice[car.CarType];
                decimal fine = Settings.Fine;
                decimal curPrice = 0;
                if (car.Balance > 0)
                {
                    if (car.Balance < parkingPrice)
                    {
                        decimal rest = car.Balance;
                        decimal negativeBalance = (parkingPrice - rest) * fine;
                        curPrice = (rest + negativeBalance);
                    }
                    else
                    {
                        curPrice = parkingPrice;
                    }
                }
                else
                {
                    curPrice = (parkingPrice * fine);
                }

                car.Balance -= curPrice;

                this.parkingBalance += curPrice;
                // Add transaction
                lock (transactionsSyncRoot)
                {
                    this.transactions.Add(new Transaction(car.Id, curPrice));
                }
            }
        }

        public bool AddBalanceCar(string id, decimal money)
        {
            if (String.IsNullOrWhiteSpace(id)) return false;

            if (money <= 0) return false;

            Car car = this.cars.FirstOrDefault(x => x.Id == id);
            if (car == null) return false;

            car.Balance += money;
            return true;
        }

        public decimal GetTotalParkingIncome() 
            => this.parkingBalance;

        public int CountFreeParkingPlaces() 
            => Settings.ParkingSpace - this.cars.Count;

        public int CountOccupiedParkingPlaces() 
            => this.cars.Count;
            

        private void WriteLogAndCleanTransactions(object o)
        {
            string path = Settings.LogPath;
            decimal sum;
            lock (transactionsSyncRoot)
            {
                sum = transactions.Sum(t => t.Debited);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("{0} - sum = {1:C2}", DateTime.Now, sum);
                }
            }
            catch (UnauthorizedAccessException e) { Console.WriteLine(e.Message); }
            catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
            catch (ArgumentException e) { Console.WriteLine(e.Message); }
            catch (DirectoryNotFoundException e) { Console.WriteLine(e.Message); }
            catch (PathTooLongException e) { Console.WriteLine(e.Message); }
            catch (IOException e) { Console.WriteLine(e.Message); }

            lock (transactionsSyncRoot)
            {
                transactions.Clear();
            }
        }

        public decimal GetIncomeLastMinute()
            => transactions.Sum(t => t.Debited);
        

        public Task<IEnumerable<Car>> GetAllCarsAsync() 
            => Task.Run(()
                =>  cars.Select(x => (Car)x.Clone()));


        public Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
            => Task.Run(()
                => transactions.Select(x => (Transaction)x.Clone()));
        

        public Task<IEnumerable<Transaction>> GetAllTransactionsByIdAsync(string id)
            => Task.Run(() =>
                { 
                    lock (transactionsSyncRoot)
                       return transactions.Where(t => t.Id == id)
                            .Select(x => (Transaction)x.Clone());   
                });

        public Task<List<string>> GetTransactionsLogAsync()
        {
            return Task.Run(() =>
            { 
                string path = Settings.LogPath;
                List<string> log = new List<string>();
                if (File.Exists(path) && !String.IsNullOrWhiteSpace(path))
                {
                    StreamReader sr;
                    try
                    {
                        using (sr = new StreamReader(path, true))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                log.Add(line);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException e) { Console.WriteLine(e.Message); }
                    catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
                    catch (ArgumentException e) { Console.WriteLine(e.Message); }
                    catch (DirectoryNotFoundException e) { Console.WriteLine(e.Message); }
                    catch (PathTooLongException e) { Console.WriteLine(e.Message); }
                    catch (IOException e) { Console.WriteLine(e.Message); }
                }
                return log;
            });
        }

    }
}
