﻿using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Linq.Expressions;
namespace Car_Rental.Data.Interfaces;
public interface IData
{
	int NextVehicleId { get; }
	int NextPersonId { get; }
	int NextBookingId { get; }
	string[] BookingStatusNames() => Enum.GetNames(typeof(BookingStatuses));
	string[] VehicleTypeNames() => Enum.GetNames(typeof(VehicleTypes));
	string[] VehicleStatusNames() => Enum.GetNames(typeof(VehicleStatuses));
	BookingStatuses GetBookingStatus(string name) => (BookingStatuses)Enum.Parse(typeof(BookingStatuses), name);
	VehicleTypes GetVehicleType(string name) => (VehicleTypes)Enum.Parse(typeof(VehicleTypes), name);
	VehicleStatuses GetVehicleStatus(string name) => (VehicleStatuses)Enum.Parse(typeof(VehicleStatuses), name);
	List<T> Get<T>(Expression<Func<T, bool>>? expression);
	T? Single<T>(Expression<Func<T, bool>>? expression);
	void Add<T>(T item);
	Task<List<IBooking>> RentVehicle(int vehicleId, int customerId);
	IBooking ReturnVehicle(int vehicleId, double distance, int days);
}