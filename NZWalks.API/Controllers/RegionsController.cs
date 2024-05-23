using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to dtos
            //var regionsDto = new List<RegionDto>();
            //foreach(var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}

            //Return Dtos
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

        }

        //Get region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region DomainModel from Database

            var regionDomain = await regionRepository.GetByIdAsync(id);

             if(regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Regio Dto
            //
            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

             //Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //Post to create  a new Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
               

            //Use DomainModel To Create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //map DomainModel again To Dto

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        //Update Rgion
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //Map Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            //check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id,regionDomainModel);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert DomainModel again To Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        //Delete Region

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //RETURN DELETED REGION BACK
            //Map DomainModel To Dto

            return Ok(mapper.Map<RegionDto>(regionDomainModel));



        }

    }

}
    

