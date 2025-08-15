using Domain.Entities;
using System.Collections.Generic;

public class ModifyCheckinsRequest
{
    public string Id { get; set; }  // Bisa kosong untuk insert
    public List<Checkin> Datas { get; set; }  // List supaya bisa .Any()
}
