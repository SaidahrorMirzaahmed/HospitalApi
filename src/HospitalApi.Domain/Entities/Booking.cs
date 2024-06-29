using HospitalApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalApi.Domain.Commons;
using HospitalApi.Domain.Entities;

namespace HospitalApi.Domain.Entities;

public class Booking : Auditable
{
    public long StaffId {  get; set; }
    public User Staff { get; set; }
    public long ClientId {  get; set; }
    public User Client { get; set; }
    public TimesOfBooking Date {  get; set; }
}
