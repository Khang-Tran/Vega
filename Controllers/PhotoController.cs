using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistent.Repositories;
using Vega.Persistent.UnitOfWork;

namespace Vega.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotoController: Controller
    {
        
        private readonly IHostingEnvironment _host;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _photoRepository;
        private readonly PhotoSettings photoSettings;

        public PhotoController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photo)
        {
            this.photoSettings = options.Value;
            _host = host;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoRepository = photo;
        }
        [HttpPost]
        public async Task<IActionResult> UpLoad(int vehicleId, IFormFile file)
        {
            var vehicle =await _vehicleRepository.GetVehicle(vehicleId, false);
            if (vehicle == null)
                return NotFound("");

            if (file == null)
                return BadRequest("Null file");
            if (file.Length == 0)
                return BadRequest("Empty file");

            if (file.Length > photoSettings.MaxBytes)
                return BadRequest("Max size is " + photoSettings.MaxBytes);

            if (!photoSettings.isSupported(file.FileName))
                return BadRequest("Invalid file type");
            

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<Photo, PhotoResource>(photo));
        }

        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await _photoRepository.GetPhotos(vehicleId);
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
    }

}
