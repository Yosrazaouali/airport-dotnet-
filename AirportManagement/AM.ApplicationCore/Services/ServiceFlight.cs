using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class ServiceFlight : IServiceFlight
    {

        List<Flight> Flights = new List<Flight>();
        public List<DateTime> GetFlightDates(string destination)
        {
            List<DateTime> ls = new List<DateTime>();
            //With for structure
            //for (int j = 0; j < Flights.Count; j++)
            //    if (Flights[j].Destination.Equals(destination))
            //        ls.Add(Flights[j].FlightDate);

            //With foreach structure
            //foreach(Flight f in Flights)
            //    if (f.Destination.Equals(destination))
            //        ls.Add(f.FlightDate);
            //return ls;

            //with LINQ language
            var query = from f in Flights
                        where
                        f.Destination.Equals(destination)
                        select f.FlightDate;
            return query.ToList();
            //with Lamda
            ///return Flights.where(p=>p.Destiniation==destination).select(p=>p.Flight.Date);
        }

        public void GetFlights(string filterType, string filterValue)
        {

        }

        public void ShowFlightDetails(Plane plane)
        {
            //var req = from f in Flights
            //          where f.Plane == plane
            //          select new { f.FlightDate, f.Destination };

            var req = Flights.Where(req => req.Plane == plane).Select(p => new { p.FlightDate, p.Destination });
            foreach (var v in req)
                Console.WriteLine("Flight Date; " + v.FlightDate + " Flight destination: " + v.Destination);
        }

        public int ProgrammedFlightNumber(DateTime startDate)
        {
            //var req = from f in Flights
            //          where (f.FlightDate < startDate) && f.FlightDate <= startDate.AddDays(7)
            //          // where ( f.FlightDate - startDate ).TotalDays<7
            //          select f;
            //return req.Count();

            var req = Flights.Where((req => req.FlightDate <= startDate.AddDays(7))).Count();
            return 0;


        }
        public double DurationAverage(string destination)
        {
            //var req = from f in Flights
            //          where f.Destination.Equals(destination)
            //          select f.EstimatedDuration;    
            //return req.Average();

            return Flights.Where(req => req.Destination == destination).Select(p => p.EstimatedDuration).Average();
            // return Flights.Where(req => req.Destination == destination).Average(p => p.EstimatedDuration); 
        }
        public IEnumerable<Flight> OrderedDurationFlights()
        //public IList<Flight> OrderedDurationFlights()


        {
            //var query = from f in Flights
            //            orderby f.EstimatedDuration descending
            //            select f;
            ////return query.toList;
            //return query;

            return Flights.OrderByDescending(f => f.EstimatedDuration);
        }
        IEnumerable<Traveller> SeniorTravellers(Flight flight)
        //{
        //    //var query = from f in flight.Passengers.OfType<Traveller>()
        //    //            orderby f.BirthDate
        //    //            descending
        //    //            select f;
        //    //return query.OfType<Traveller>().Take(3);
        //}
        { return flight.Passengers.OfType<Traveller>().OrderByDescending(f => f.BirthDate).Take(3); }

    }
}
