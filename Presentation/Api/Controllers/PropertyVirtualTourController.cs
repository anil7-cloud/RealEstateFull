using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/properties")]
    public class PropertyVirtualTourController :
        ControllerBase
    {
        private readonly PropertyVirtualTourService
            _service;

        private readonly PropertyVirtualTourViewerService
            _viewerService;

        private readonly PropertyVirtualTourUploadService
            _uploadService;

        public PropertyVirtualTourController(
            PropertyVirtualTourService service,
            PropertyVirtualTourViewerService viewerService,
            PropertyVirtualTourUploadService uploadService)
        {
            _service = service;
            _viewerService = viewerService;
            _uploadService = uploadService;
        }

        [HttpPost("{propertyId:int}/virtual-tours/panorama")]
        [RequestSizeLimit(31457280)]
        public async Task<IActionResult>
            CreatePanoramaTour(
                int propertyId,
                [FromForm] IFormFile file,
                [FromForm] string? title,
                [FromForm] string? sceneName,
                CancellationToken cancellationToken)
        {
            try
            {
                if (propertyId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "PropertyId geçersiz."
                    });
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest(new
                    {
                        message = "Panorama dosyası zorunludur."
                    });
                }

                // 1. Panorama dosyasını kaydet.
                var upload =
                    await _uploadService
                        .UploadPanoramaAsync(
                            propertyId,
                            file,
                            cancellationToken);

                // 2. Virtual tour oluştur.
                var tour =
                    await _service.CreateAsync(
                        propertyId,
                        new CreatePropertyVirtualTourRequest
                        {
                            Title =
                                string.IsNullOrWhiteSpace(title)
                                    ? "360° Sanal Tur"
                                    : title.Trim(),

                            TourType =
                                "Panorama360",

                            ThumbnailUrl =
                                upload.Url,

                            DisplayOrder =
                                0
                        },
                        cancellationToken);

                // 3. İlk panorama sahnesini oluştur.
                var scene =
                    await _service.AddSceneAsync(
                        tour.Id,
                        new CreatePropertyVirtualTourSceneRequest
                        {
                            Name =
                                string.IsNullOrWhiteSpace(sceneName)
                                    ? "Başlangıç"
                                    : sceneName.Trim(),

                            PanoramaUrl =
                                upload.Url,

                            ThumbnailUrl =
                                upload.Url,

                            InitialYaw =
                                0m,

                            InitialPitch =
                                0m,

                            InitialFov =
                                90m,

                            DisplayOrder =
                                0,

                            IsStartScene =
                                true
                        },
                        cancellationToken);

                return Ok(new
                {
                    message =
                        "360° sanal tur başarıyla oluşturuldu.",

                    propertyId,

                    upload,

                    tourId =
                        tour.Id,

                    sceneId =
                        scene.Id,

                    panoramaUrl =
                        upload.Url
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }



        [HttpPost("{propertyId:int}/virtual-tour-files/panorama")]
        [RequestSizeLimit(31457280)]
        public async Task<IActionResult>
            UploadPanorama(
                int propertyId,
                IFormFile file,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _uploadService
                        .UploadPanoramaAsync(
                            propertyId,
                            file,
                            cancellationToken);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPost("{propertyId:int}/virtual-tour-files/model")]
        [RequestSizeLimit(157286400)]
        public async Task<IActionResult>
            Upload3DModel(
                int propertyId,
                IFormFile file,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _uploadService
                        .UploadModelAsync(
                            propertyId,
                            file,
                            cancellationToken);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPost("{propertyId:int}/virtual-tours")]
        public async Task<IActionResult>
            CreateTour(
                int propertyId,
                [FromBody]
                    CreatePropertyVirtualTourRequest request,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _service.CreateAsync(
                        propertyId,
                        request,
                        cancellationToken);

                return CreatedAtAction(
                    nameof(GetTour),
                    new
                    {
                        tourId = result.Id
                    },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("{propertyId:int}/virtual-tours")]
        public async Task<IActionResult>
            GetPropertyTours(
                int propertyId,
                CancellationToken cancellationToken)
        {
            var result =
                await _service
                    .GetPropertyToursAsync(
                        propertyId,
                        cancellationToken);

            return Ok(result);
        }


        [HttpGet("virtual-tours/{tourId:guid}/viewer")]
        public async Task<IActionResult>
            GetViewer(
                Guid tourId,
                CancellationToken cancellationToken)
        {
            var viewer =
                await _viewerService.GetViewerAsync(
                    tourId,
                    cancellationToken);

            if (viewer == null)
            {
                return NotFound(new
                {
                    message =
                        "360 derece virtual tour bulunamadı."
                });
            }

            return Ok(viewer);
        }

        [HttpGet("virtual-tours/{tourId:guid}")]
        public async Task<IActionResult>
            GetTour(
                Guid tourId,
                CancellationToken cancellationToken)
        {
            var result =
                await _service.GetAsync(
                    tourId,
                    cancellationToken);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Virtual tour bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpPost("virtual-tours/{tourId:guid}/scenes")]
        public async Task<IActionResult>
            AddScene(
                Guid tourId,
                [FromBody]
                    CreatePropertyVirtualTourSceneRequest request,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _service.AddSceneAsync(
                        tourId,
                        request,
                        cancellationToken);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("virtual-tour-scenes/{sceneId:guid}/hotspots")]
        public async Task<IActionResult>
            AddHotspot(
                Guid sceneId,
                [FromBody]
                    CreatePropertyVirtualTourHotspotRequest request,
                CancellationToken cancellationToken)
        {
            var result =
                await _service.AddHotspotAsync(
                    sceneId,
                    request,
                    cancellationToken);

            return Ok(result);
        }

        [HttpDelete("virtual-tour-hotspots/{hotspotId:guid}")]
        public async Task<IActionResult>
            DeleteHotspot(
                Guid hotspotId,
                CancellationToken cancellationToken)
        {
            try
            {
                await _service
                    .DeleteHotspotAsync(
                        hotspotId,
                        cancellationToken);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("virtual-tour-hotspots/{hotspotId:guid}")]
        public async Task<IActionResult>
            UpdateHotspot(
                Guid hotspotId,
                [FromBody]
                    CreatePropertyVirtualTourHotspotRequest request,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _service
                        .UpdateHotspotAsync(
                            hotspotId,
                            request,
                            cancellationToken);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }



        [HttpPost("virtual-tours/{tourId:guid}/models")]
        public async Task<IActionResult>
            Add3DModel(
                Guid tourId,
                [FromBody]
                    CreateProperty3DModelRequest request,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _service.AddModelAsync(
                        tourId,
                        request,
                        cancellationToken);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
