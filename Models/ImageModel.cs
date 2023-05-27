using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace DPOBackend.Models;

[PrimaryKey("Id")]
public class ImageModel
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public int Size { get; set; }
    public byte[] Data { get; set; }
}