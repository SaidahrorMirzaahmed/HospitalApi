﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalApi.Domain.Commons;
using HospitalApi.Domain.Entities;

namespace HospitalApi.Domain.Entities;

public class NewsList : Auditable
{
    public long PublisherId { get; set; }
    public User Publisher { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public long? PictureId { get; set; }
    public Asset? Picture { get; set; }
}
