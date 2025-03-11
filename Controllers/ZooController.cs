
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagement.Database;
using ZooManagement.Models;

namespace ZooManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ZooController:ControllerBase{

    private readonly ILogger<ZooController> _logger;

    private ZooContext _zooContext;
    public ZooController(ILogger<ZooController> logger, ZooContext zooContext){
        _logger = logger;
        _zooContext = zooContext;

    }

    [HttpGet("{id}")]
    // [Route("{id}")]
    public Animal? Get(int? id){
        return _zooContext.Animal.Include(animal=>animal.Species).FirstOrDefault(a=>a.Id == id);   
    }
}