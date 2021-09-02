using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        // GET: api/<ValuesController>
        public List<PortDetail> portDetails;
        public List<ShipDetails> shipDetails;
        public ShipsController()
        {
            readData();
        }
        public void readData()
        {
            readandwrite rd = new readandwrite();
            portDetails = JsonConvert.DeserializeObject<List<PortDetail>>(rd.ConvertCsvFileToJsonObject("./data.csv"));
            shipDetails = rd.LoadJson();
        }

        [HttpGet]
        public object Get()
        {
            readData();
            return shipDetails;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ShipDetails Get(string id)
        {
            readData();
            for(var i = 0; i<shipDetails.Count;i++)
            {
                if(shipDetails[i].id == id)
                {
                    return shipDetails[i];
                }
            }
            return null;
        }

        [HttpPost("/addship")]
        public string Post([FromBody] AddShip value)
        {
            if (value == null) return "not succesfully written";
            ShipDetails sD = new ShipDetails();
            WriteData wdata = new WriteData();
            Guid g = Guid.NewGuid();
            sD.id = g.ToString();
            sD.latitude = value.latitude;
            sD.longitude = value.longitude;
            sD.name = value.name;
            sD.velocity = value.velocity;
            sD.nearestportinKm = wdata.getNearestPort(sD, portDetails).portname;
            sD.timeToReachNearestPortinHrs = wdata.GetTime(sD, wdata.getNearestPort(sD, portDetails));
            wdata.writeDataToJson(sD,false);
            return "succesfully added";
        }

        // PUT api/<ValuesController>/5
        [HttpPut("/updateshipvelocity/{id}")]
        public string Put(string id, [FromBody] ShipDetails value)
        {
            WriteData wdata = new WriteData();
            for (var i =0; i<shipDetails.Count; i++)
            {
                if(shipDetails[i].id == id)
                {                    
                    shipDetails[i].velocity = value.velocity;
                    shipDetails[i].timeToReachNearestPortinHrs = wdata.GetTime(shipDetails[i], wdata.getNearestPort(shipDetails[i], portDetails));
                    wdata.writeDataToJson(shipDetails[i],true);
                    return "succesfully updated";
                }
            }
            return "not successfully updated";
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
