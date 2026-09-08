const parameters =
    new URLSearchParams(
        window.location.search);

const propertyId =
    parameters.get(
        "propertyId");


const propertyTitle =
    document.getElementById(
        "propertyTitle");

const mediaStatus =
    document.getElementById(
        "mediaStatus");

const mainPhoto =
    document.getElementById(
        "mainPhoto");

const photoEmpty =
    document.getElementById(
        "photoEmpty");

const photoThumbnails =
    document.getElementById(
        "photoThumbnails");

const previousPhoto =
    document.getElementById(
        "previousPhoto");

const nextPhoto =
    document.getElementById(
        "nextPhoto");

const tour360Frame =
    document.getElementById(
        "tour360Frame");

const tour360Empty =
    document.getElementById(
        "tour360Empty");

const model3dFrame =
    document.getElementById(
        "model3dFrame");

const model3dEmpty =
    document.getElementById(
        "model3dEmpty");


let tours = [];

let photos = [];

let currentPhotoIndex = 0;

let panoramaTour = null;

let modelTour = null;


function setMode(
    mode)
{
    document
        .querySelectorAll(
            ".tab")
        .forEach(
            button =>
            {
                button.classList.toggle(
                    "active",
                    button.dataset.mode ===
                        mode);
            });

    document
        .querySelectorAll(
            ".media-panel")
        .forEach(
            panel =>
            {
                panel.classList.remove(
                    "active");
            });

    if (mode === "photos")
    {
        document
            .getElementById(
                "photosPanel")
            .classList.add(
                "active");
    }

    if (mode === "tour360")
    {
        document
            .getElementById(
                "tour360Panel")
            .classList.add(
                "active");

        load360Frame();
    }

    if (mode === "model3d")
    {
        document
            .getElementById(
                "model3dPanel")
            .classList.add(
                "active");

        load3DFrame();
    }
}


function load360Frame()
{
    if (!panoramaTour)
    {
        tour360Frame.style.display =
            "none";

        tour360Empty.style.display =
            "block";

        return;
    }

    tour360Empty.style.display =
        "none";

    tour360Frame.style.display =
        "block";

    if (!tour360Frame.src)
    {
        tour360Frame.src =
            `/virtual-tour/viewer.html?tourId=${encodeURIComponent(
                panoramaTour.id)}`;
    }
}


function load3DFrame()
{
    if (!modelTour)
    {
        model3dFrame.style.display =
            "none";

        model3dEmpty.style.display =
            "block";

        return;
    }

    model3dEmpty.style.display =
        "none";

    model3dFrame.style.display =
        "block";

    if (!model3dFrame.src)
    {
        model3dFrame.src =
            `/virtual-tour/model-viewer.html?tourId=${encodeURIComponent(
                modelTour.id)}`;
    }
}


function showPhoto(
    index)
{
    if (photos.length === 0)
    {
        mainPhoto.style.display =
            "none";

        photoEmpty.style.display =
            "block";

        previousPhoto.style.display =
            "none";

        nextPhoto.style.display =
            "none";

        return;
    }

    currentPhotoIndex =
        (
            index +
            photos.length
        ) %
        photos.length;

    const photo =
        photos[
            currentPhotoIndex
        ];

    mainPhoto.src =
        typeof photo === "string"
            ? photo
            : (
                photo.url ||
                photo.imageUrl ||
                photo.photoUrl
            );

    mainPhoto.style.display =
        "block";

    photoEmpty.style.display =
        "none";

    previousPhoto.style.display =
        photos.length > 1
            ? "block"
            : "none";

    nextPhoto.style.display =
        photos.length > 1
            ? "block"
            : "none";

    document
        .querySelectorAll(
            ".thumbnail")
        .forEach(
            (thumbnail, i) =>
            {
                thumbnail.classList.toggle(
                    "active",
                    i ===
                        currentPhotoIndex);
            });
}


function createPhotoThumbnails()
{
    photoThumbnails.innerHTML =
        "";

    photos.forEach(
        (photo, index) =>
        {
            const url =
                typeof photo === "string"
                    ? photo
                    : (
                        photo.url ||
                        photo.imageUrl ||
                        photo.photoUrl
                    );

            if (!url)
                return;

            const image =
                document.createElement(
                    "img");

            image.src =
                url;

            image.className =
                "thumbnail";

            image.alt =
                `İlan fotoğrafı ${index + 1}`;

            image.addEventListener(
                "click",
                () =>
                    showPhoto(
                        index));

            photoThumbnails
                .appendChild(
                    image);
        });
}


function detectTourTypes()
{
    /*
     * Property 1 için geliştirme sırasında kullandığımız
     * ana 360 derece turu öncelikli seç.
     */
    const preferredTourId =
        "9edd42d9-18f3-4904-9b7c-2609db9ce383";

    panoramaTour =
        tours.find(
            tour =>
                String(tour.id)
                    .toLowerCase() ===
                preferredTourId
                    .toLowerCase()
        ) ||
        tours.find(
            tour =>
                tour.tourType
                    ?.toLowerCase() ===
                    "panorama360" ||
                tour.tourType
                    ?.toLowerCase() ===
                    "virtualtour");

    console.log(
        "AKTIF 360 TUR:",
        panoramaTour?.id,
        panoramaTour);

    modelTour =
        tours.find(
            tour =>
                tour.tourType
                    ?.toLowerCase() ===
                    "model3d" ||
                (
                    tour.models &&
                    tour.models.length > 0
                ));


    const tourButton =
        document.querySelector(
            '[data-mode="tour360"]');

    const modelButton =
        document.querySelector(
            '[data-mode="model3d"]');

    tourButton.disabled =
        !panoramaTour;

    modelButton.disabled =
        !modelTour;
}


async function loadTours()
{
    const apiUrl =
        `/api/properties/${propertyId}/virtual-tours`;

    console.log("GET:", apiUrl);

    const response =
        await fetch(
            apiUrl,
            {
                cache: "no-store",
                headers: {
                    "Accept": "application/json"
                }
            });

    const contentType =
        response.headers.get("content-type") || "";

    console.log(
        "STATUS:",
        response.status,
        "CONTENT-TYPE:",
        contentType
    );

    const body =
        await response.text();

    console.log(
        "BODY:",
        body.substring(0, 500)
    );

    if (!response.ok)
    {
        throw new Error(
            `API HTTP ${response.status}: ${body.substring(0, 120)}`
        );
    }

    if (!contentType.includes("application/json"))
    {
        throw new Error(
            `JSON bekleniyordu fakat ${contentType || "Content-Type yok"} geldi.`
        );
    }

    try
    {
        tours =
            body.trim()
                ? JSON.parse(body)
                : [];
    }
    catch (error)
    {
        throw new Error(
            `JSON okunamadı: ${error.message}`
        );
    }

    if (!Array.isArray(tours))
    {
        throw new Error(
            "Virtual tour API bir JSON array döndürmedi."
        );
    }

    detectTourTypes();
}


async function loadProperty()
{
    /*
     * Projedeki mevcut Property endpoint'inden
     * ilan fotoğraflarını almaya çalışıyoruz.
     *
     * Endpoint yapın farklıysa yalnızca buradaki
     * URL/property alanını değiştirmek yeterli.
     */

    try
    {
        const response =
            await fetch(
                `/api/properties/${propertyId}`,
                {
                    headers: {
                        "Accept": "application/json"
                    }
                });

        if (!response.ok)
        {
            console.warn(
                "Property API HTTP:",
                response.status);

            return;
        }

        const contentType =
            response.headers.get(
                "content-type") || "";

        if (!contentType.includes(
                "application/json"))
        {
            const body =
                await response.text();

            console.error(
                "Property endpoint JSON yerine başka içerik döndürdü:",
                body.substring(0, 200));

            return;
        }

        const property =
            await response.json();

        propertyTitle.textContent =
            property.title ||
            property.name ||
            "İlan Görüntüleme";

        photos =
            property.photos ||
            property.images ||
            property.imageUrls ||
            [];

        createPhotoThumbnails();

        showPhoto(0);
    }
    catch
    {
        photos = [];

        showPhoto(0);
    }
}


document
    .querySelectorAll(
        ".tab")
    .forEach(
        button =>
        {
            button.addEventListener(
                "click",
                () =>
                {
                    if (
                        button.disabled
                    )
                        return;

                    setMode(
                        button.dataset.mode);
                });
        });


previousPhoto
    .addEventListener(
        "click",
        () =>
            showPhoto(
                currentPhotoIndex - 1));


nextPhoto
    .addEventListener(
        "click",
        () =>
            showPhoto(
                currentPhotoIndex + 1));


document
    .getElementById(
        "fullscreenButton")
    .addEventListener(
        "click",
        async () =>
        {
            const root =
                document.getElementById(
                    "propertyMedia");

            if (
                !document.fullscreenElement
            )
            {
                await root
                    .requestFullscreen();
            }
            else
            {
                await document
                    .exitFullscreen();
            }
        });


async function uploadPanorama()
{
    const fileInput =
        document.getElementById("panoramaFile");

    const titleInput =
        document.getElementById("panoramaTitle");

    const sceneInput =
        document.getElementById("panoramaSceneName");

    const button =
        document.getElementById("uploadPanoramaButton");

    const status =
        document.getElementById("panoramaUploadStatus");

    const file =
        fileInput.files[0];

    if (!file)
    {
        status.textContent =
            "Önce bir panorama fotoğrafı seçin.";
        return;
    }

    const sceneName =
        sceneInput.value.trim() || "Yeni Oda";

    button.disabled = true;

    try
    {
        /*
         * TUR YOKSA:
         * İlk panorama ile yeni tur oluştur.
         */
        if (!panoramaTour)
        {
            status.textContent =
                "360° sanal tur oluşturuluyor...";

            const formData =
                new FormData();

            formData.append(
                "file",
                file);

            formData.append(
                "title",
                titleInput.value ||
                "360 Daire Turu");

            formData.append(
                "sceneName",
                sceneName);

            const response =
                await fetch(
                    `/api/properties/${propertyId}/virtual-tours/panorama`,
                    {
                        method: "POST",
                        body: formData
                    });

            const body =
                await response.text();

            if (!response.ok)
            {
                throw new Error(
                    `Tur oluşturulamadı: HTTP ${response.status} ${body}`
                );
            }

            const result =
                JSON.parse(body);

            await loadTours();

            panoramaTour =
                tours.find(
                    x =>
                        String(x.id).toLowerCase() ===
                        String(result.tourId).toLowerCase()
                ) ||
                panoramaTour;

            status.textContent =
                "✓ İlk panorama ve tur oluşturuldu.";
        }

        /*
         * TUR VARSA:
         * Dosyayı yükle ve mevcut tura Scene ekle.
         */
        else
        {
            status.textContent =
                `${sceneName} panoraması yükleniyor...`;

            const uploadData =
                new FormData();

            uploadData.append(
                "file",
                file);

            const uploadResponse =
                await fetch(
                    `/api/properties/${propertyId}/virtual-tour-files/panorama`,
                    {
                        method: "POST",
                        body: uploadData
                    });

            const uploadBody =
                await uploadResponse.text();

            if (!uploadResponse.ok)
            {
                throw new Error(
                    `Panorama yüklenemedi: HTTP ${uploadResponse.status} ${uploadBody}`
                );
            }

            const uploadResult =
                JSON.parse(uploadBody);

            if (!uploadResult.url)
            {
                throw new Error(
                    "Sunucu panorama URL'si döndürmedi."
                );
            }

            status.textContent =
                `${sceneName} sahnesi oluşturuluyor...`;

            const sceneResponse =
                await fetch(
                    `/api/properties/virtual-tours/${panoramaTour.id}/scenes`,
                    {
                        method: "POST",
                        headers:
                        {
                            "Content-Type":
                                "application/json"
                        },
                        body: JSON.stringify(
                        {
                            name:
                                sceneName,

                            panoramaUrl:
                                uploadResult.url,

                            thumbnailUrl:
                                uploadResult.url,

                            initialYaw:
                                0,

                            initialPitch:
                                0,

                            initialFov:
                                90,

                            displayOrder:
                                100,

                            isStartScene:
                                false
                        })
                    });

            const sceneBody =
                await sceneResponse.text();

            if (!sceneResponse.ok)
            {
                throw new Error(
                    `Sahne oluşturulamadı: HTTP ${sceneResponse.status} ${sceneBody}`
                );
            }

            const sceneResult =
                JSON.parse(sceneBody);

            console.log(
                "Yeni panorama scene:",
                sceneResult);

            status.textContent =
                `✓ ${sceneName} mevcut 360° tura eklendi.`;
        }

        /*
         * Tur listesini yenile.
         */
        await loadTours();

        mediaStatus.textContent =
            `${tours.length} sanal tur bulundu.`;

        /*
         * Viewer'ı cache kırarak yenile.
         */
        if (panoramaTour)
        {
            const currentTour =
                tours.find(
                    x =>
                        String(x.id).toLowerCase() ===
                        String(panoramaTour.id).toLowerCase()
                );

            if (currentTour)
            {
                panoramaTour =
                    currentTour;
            }

            const frame =
                document.getElementById("tour360Frame");

            frame.src =
                `/virtual-tour/viewer.html?tourId=${panoramaTour.id}&v=${Date.now()}`;

            document.getElementById(
                "tour360Empty"
            ).style.display =
                "none";

            frame.style.display =
                "block";

            setMode(
                "tour360");
        }

        fileInput.value =
            "";

        sceneInput.value =
            "";
    }

    catch (error)
    {
        console.error(error);

        status.textContent =
            error.message;
    }

    finally
    {
        button.disabled =
            false;
    }
}


function initializePanoramaUpload()
{
    const button =
        document.getElementById(
            "uploadPanoramaButton");

    if (!button)
        return;

    button.addEventListener(
        "click",
        uploadPanorama);
}



async function initialize()
{
    console.log("PROPERTY MEDIA DEBUG");
    console.log("Current URL:", window.location.href);
    console.log("propertyId:", propertyId);
    console.log(
        "Virtual Tour API:",
        `/api/properties/${propertyId}/virtual-tours`
    );

    initializePanoramaUpload();

    if (!propertyId)
    {
        mediaStatus.textContent =
            "propertyId belirtilmedi.";

        return;
    }

    try
    {
        await Promise.all([
            loadTours(),
            loadProperty()
        ]);

        mediaStatus.textContent =
            `${tours.length} sanal tur bulundu.`;

        setMode(
            "photos");
    }
    catch (error)
    {
        console.error(
            error);

        mediaStatus.textContent =
            error.message;
    }
}


initialize();


/*
 * ==========================================================
 * 360° HOTSPOT / ODA GEÇİŞİ YÖNETİMİ
 * ==========================================================
 */

async function loadHotspotScenes()
{
    const sourceSelect =
        document.getElementById("hotspotSourceScene");

    const targetSelect =
        document.getElementById("hotspotTargetScene");

    if (!sourceSelect || !targetSelect)
        return;

    sourceSelect.innerHTML =
        '<option value="">Oda seçin</option>';

    targetSelect.innerHTML =
        '<option value="">Oda seçin</option>';

    if (!panoramaTour)
        return;

    try
    {
        const response =
            await fetch(
                `/api/properties/virtual-tours/${panoramaTour.id}/viewer`
            );

        if (!response.ok)
        {
            throw new Error(
                `Tur bilgisi alınamadı: HTTP ${response.status}`
            );
        }

        const viewer =
            await response.json();

        const scenes =
            viewer.scenes || [];

        for (const scene of scenes)
        {
            const sourceOption =
                document.createElement("option");

            sourceOption.value =
                scene.id;

            sourceOption.textContent =
                scene.name;

            sourceSelect.appendChild(
                sourceOption);

            const targetOption =
                document.createElement("option");

            targetOption.value =
                scene.id;

            targetOption.textContent =
                scene.name;

            targetSelect.appendChild(
                targetOption);
        }

        console.log(
            "Hotspot sahneleri:",
            scenes);
    }
    catch (error)
    {
        console.error(
            "Hotspot scene yükleme hatası:",
            error);
    }
}


async function addHotspot()
{
    const sourceSelect =
        document.getElementById("hotspotSourceScene");

    const targetSelect =
        document.getElementById("hotspotTargetScene");

    const titleInput =
        document.getElementById("hotspotTitle");

    const yawInput =
        document.getElementById("hotspotYaw");

    const pitchInput =
        document.getElementById("hotspotPitch");

    const status =
        document.getElementById("hotspotEditorStatus");

    const button =
        document.getElementById("addHotspotButton");

    const sourceSceneId =
        sourceSelect?.value;

    const targetSceneId =
        targetSelect?.value;

    if (!sourceSceneId)
    {
        status.textContent =
            "Kaynak oda seçin.";
        return;
    }

    if (!targetSceneId)
    {
        status.textContent =
            "Hedef oda seçin.";
        return;
    }

    if (sourceSceneId === targetSceneId)
    {
        status.textContent =
            "Kaynak ve hedef oda aynı olamaz.";
        return;
    }

    const targetName =
        targetSelect.options[
            targetSelect.selectedIndex
        ]?.textContent || "Oda";

    const title =
        titleInput.value.trim() ||
        `${targetName} Git`;

    const yaw =
        Number(yawInput.value || 0);

    const pitch =
        Number(pitchInput.value || 0);

    button.disabled =
        true;

    status.textContent =
        "Hotspot ekleniyor...";

    try
    {
        const response =
            await fetch(
                `/api/properties/virtual-tour-scenes/${sourceSceneId}/hotspots`,
                {
                    method: "POST",

                    headers:
                    {
                        "Content-Type":
                            "application/json"
                    },

                    body:
                        JSON.stringify(
                        {
                            title:
                                title,

                            hotspotType:
                                "Scene",

                            targetSceneId:
                                targetSceneId,

                            yaw:
                                yaw,

                            pitch:
                                pitch,

                            description:
                                targetName
                        })
                });

        const body =
            await response.text();

        if (!response.ok)
        {
            throw new Error(
                `Hotspot eklenemedi: HTTP ${response.status} ${body}`
            );
        }

        console.log(
            "Hotspot oluşturuldu:",
            body);

        status.textContent =
            `✓ ${title} geçişi oluşturuldu.`;

        /*
         * Viewer'ı yenile.
         */
        if (panoramaTour)
        {
            const frame =
                document.getElementById(
                    "tour360Frame");

            frame.src =
                `/virtual-tour/viewer.html?tourId=${panoramaTour.id}&v=${Date.now()}`;
        }

        titleInput.value =
            "";
    }
    catch (error)
    {
        console.error(error);

        status.textContent =
            error.message;
    }
    finally
    {
        button.disabled =
            false;
    }
}


function initializeHotspotEditor()
{
    const button =
        document.getElementById(
            "addHotspotButton");

    if (!button)
        return;

    button.addEventListener(
        "click",
        addHotspot);

    loadHotspotScenes();

    console.log(
        "360 hotspot editörü aktif.");
}


/*
 * Sayfa hazır olduğunda hotspot editörünü başlat.
 */
window.addEventListener(
    "load",
    () =>
    {
        setTimeout(
            loadHotspotScenes,
            1000);

        initializeHotspotEditor();
    });



/*
 * ============================================================
 * HOTSPOT SAHNE LISTESI - GUARANTEED LOAD
 * ============================================================
 */

async function refreshHotspotSceneSelectors()
{
    const source =
        document.getElementById(
            "hotspotSourceScene");

    const target =
        document.getElementById(
            "hotspotTargetScene");

    const status =
        document.getElementById(
            "hotspotEditorStatus");

    if (!source || !target)
    {
        console.warn(
            "Hotspot select elementleri bulunamadı.");

        return;
    }

    source.innerHTML =
        '<option value="">Oda seçin</option>';

    target.innerHTML =
        '<option value="">Oda seçin</option>';

    if (!panoramaTour || !panoramaTour.id)
    {
        if (status)
        {
            status.textContent =
                "Önce bir 360° tur seçilmeli.";
        }

        return;
    }

    try
    {
        console.log(
            "Hotspot için tur yükleniyor:",
            panoramaTour.id);

        const response =
            await fetch(
                `/api/properties/virtual-tours/${panoramaTour.id}/viewer`,
                {
                    cache: "no-store"
                });

        if (!response.ok)
        {
            throw new Error(
                `Viewer API HTTP ${response.status}`
            );
        }

        const data =
            await response.json();

        console.log(
            "Viewer data:",
            data);

        const scenes =
            Array.isArray(data.scenes)
                ? data.scenes
                : [];

        if (scenes.length === 0)
        {
            if (status)
            {
                status.textContent =
                    "Bu turda oda bulunamadı.";
            }

            return;
        }

        for (const scene of scenes)
        {
            const sourceOption =
                document.createElement(
                    "option");

            sourceOption.value =
                scene.id;

            sourceOption.textContent =
                scene.name;

            source.appendChild(
                sourceOption);


            const targetOption =
                document.createElement(
                    "option");

            targetOption.value =
                scene.id;

            targetOption.textContent =
                scene.name;

            target.appendChild(
                targetOption);
        }

        if (status)
        {
            status.textContent =
                `✓ ${scenes.length} oda yüklendi.`;
        }

        console.log(
            "Hotspot odaları yüklendi:",
            scenes.map(
                x => ({
                    id: x.id,
                    name: x.name
                })));
    }
    catch (error)
    {
        console.error(
            "Hotspot oda yükleme hatası:",
            error);

        if (status)
        {
            status.textContent =
                "Odalar yüklenemedi: " +
                error.message;
        }
    }
}


/*
 * loadTours tamamlandıktan sonra panoramaTour'un
 * oluşması için kısa sürelerle tekrar kontrol eder.
 */
async function waitAndLoadHotspotScenes()
{
    for (let attempt = 0;
         attempt < 10;
         attempt++)
    {
        if (panoramaTour &&
            panoramaTour.id)
        {
            await refreshHotspotSceneSelectors();

            return;
        }

        await new Promise(
            resolve =>
                setTimeout(
                    resolve,
                    300));
    }

    console.warn(
        "panoramaTour yüklenemedi.");
}


window.addEventListener(
    "load",
    () =>
    {
        waitAndLoadHotspotScenes();
    });



/*
 * ============================================================
 * HOTSPOT SAHNE LISTESI - GUARANTEED LOAD
 * ============================================================
 */

async function refreshHotspotSceneSelectors()
{
    const source =
        document.getElementById(
            "hotspotSourceScene");

    const target =
        document.getElementById(
            "hotspotTargetScene");

    const status =
        document.getElementById(
            "hotspotEditorStatus");

    if (!source || !target)
    {
        console.warn(
            "Hotspot select elementleri bulunamadı.");

        return;
    }

    source.innerHTML =
        '<option value="">Oda seçin</option>';

    target.innerHTML =
        '<option value="">Oda seçin</option>';

    if (!panoramaTour || !panoramaTour.id)
    {
        if (status)
        {
            status.textContent =
                "Önce bir 360° tur seçilmeli.";
        }

        return;
    }

    try
    {
        console.log(
            "Hotspot için tur yükleniyor:",
            panoramaTour.id);

        const response =
            await fetch(
                `/api/properties/virtual-tours/${panoramaTour.id}/viewer`,
                {
                    cache: "no-store"
                });

        if (!response.ok)
        {
            throw new Error(
                `Viewer API HTTP ${response.status}`
            );
        }

        const data =
            await response.json();

        console.log(
            "Viewer data:",
            data);

        const scenes =
            Array.isArray(data.scenes)
                ? data.scenes
                : [];

        if (scenes.length === 0)
        {
            if (status)
            {
                status.textContent =
                    "Bu turda oda bulunamadı.";
            }

            return;
        }

        for (const scene of scenes)
        {
            const sourceOption =
                document.createElement(
                    "option");

            sourceOption.value =
                scene.id;

            sourceOption.textContent =
                scene.name;

            source.appendChild(
                sourceOption);


            const targetOption =
                document.createElement(
                    "option");

            targetOption.value =
                scene.id;

            targetOption.textContent =
                scene.name;

            target.appendChild(
                targetOption);
        }

        if (status)
        {
            status.textContent =
                `✓ ${scenes.length} oda yüklendi.`;
        }

        console.log(
            "Hotspot odaları yüklendi:",
            scenes.map(
                x => ({
                    id: x.id,
                    name: x.name
                })));
    }
    catch (error)
    {
        console.error(
            "Hotspot oda yükleme hatası:",
            error);

        if (status)
        {
            status.textContent =
                "Odalar yüklenemedi: " +
                error.message;
        }
    }
}


/*
 * loadTours tamamlandıktan sonra panoramaTour'un
 * oluşması için kısa sürelerle tekrar kontrol eder.
 */
async function waitAndLoadHotspotScenes()
{
    for (let attempt = 0;
         attempt < 10;
         attempt++)
    {
        if (panoramaTour &&
            panoramaTour.id)
        {
            await refreshHotspotSceneSelectors();

            return;
        }

        await new Promise(
            resolve =>
                setTimeout(
                    resolve,
                    300));
    }

    console.warn(
        "panoramaTour yüklenemedi.");
}


window.addEventListener(
    "load",
    () =>
    {
        waitAndLoadHotspotScenes();
    });



/*
 * ============================================================
 * HOTSPOT LISTELEME VE SILME
 * ============================================================
 */

async function loadHotspotList()
{
    const container =
        document.getElementById(
            "hotspotList");

    if (!container)
        return;

    if (!panoramaTour ||
        !panoramaTour.id)
    {
        container.textContent =
            "Aktif 360° tur bulunamadı.";

        return;
    }

    container.textContent =
        "Geçişler yükleniyor...";

    try
    {
        const response =
            await fetch(
                `/api/properties/virtual-tours/${panoramaTour.id}/viewer?v=${Date.now()}`,
                {
                    cache: "no-store"
                });

        if (!response.ok)
        {
            throw new Error(
                `Viewer API HTTP ${response.status}`);
        }

        const data =
            await response.json();

        const scenes =
            Array.isArray(data.scenes)
                ? data.scenes
                : [];

        container.innerHTML = "";

        let hotspotCount = 0;

        for (const scene of scenes)
        {
            const hotspots =
                Array.isArray(scene.hotspots)
                    ? scene.hotspots
                    : [];

            for (const hotspot of hotspots)
            {
                hotspotCount++;

                const targetScene =
                    scenes.find(
                        x =>
                            String(x.id)
                                .toLowerCase() ===
                            String(
                                hotspot.targetSceneId || "")
                                .toLowerCase());

                const row =
                    document.createElement(
                        "div");

                row.style.display =
                    "flex";

                row.style.alignItems =
                    "center";

                row.style.justifyContent =
                    "space-between";

                row.style.gap =
                    "12px";

                row.style.padding =
                    "10px";

                row.style.margin =
                    "8px 0";

                row.style.border =
                    "1px solid #ddd";

                row.style.borderRadius =
                    "8px";

                const text =
                    document.createElement(
                        "span");

                const sourceName =
                    scene.name ||
                    "Bilinmeyen oda";

                const targetName =
                    targetScene?.name ||
                    "Hedef yok";

                text.textContent =
                    `${sourceName} → ${targetName} — ${hotspot.title || "Geçiş"}`;

                const deleteButton =
                    document.createElement(
                        "button");

                deleteButton.type =
                    "button";

                deleteButton.textContent =
                    "Sil";

                deleteButton.dataset.hotspotId =
                    hotspot.id;

                deleteButton.addEventListener(
                    "click",
                    async () =>
                    {
                        await deleteHotspot(
                            hotspot.id,
                            sourceName,
                            targetName);
                    });

                row.appendChild(text);
                row.appendChild(
                    deleteButton);

                container.appendChild(row);
            }
        }

        if (hotspotCount === 0)
        {
            container.textContent =
                "Bu turda oda geçişi bulunmuyor.";
        }

        console.log(
            "Hotspot listesi yüklendi:",
            hotspotCount);
    }
    catch (error)
    {
        console.error(
            "Hotspot listesi yüklenemedi:",
            error);

        container.textContent =
            "Geçişler yüklenemedi: " +
            error.message;
    }
}


async function deleteHotspot(
    hotspotId,
    sourceName,
    targetName)
{
    const status =
        document.getElementById(
            "hotspotEditorStatus");

    const confirmed =
        window.confirm(
            `${sourceName} → ${targetName} geçişi silinsin mi?`);

    if (!confirmed)
        return;

    try
    {
        if (status)
        {
            status.textContent =
                "Hotspot siliniyor...";
        }

        const response =
            await fetch(
                `/api/properties/virtual-tour-hotspots/${hotspotId}`,
                {
                    method: "DELETE"
                });

        if (!response.ok)
        {
            const body =
                await response.text();

            throw new Error(
                `Silinemedi: HTTP ${response.status} ${body}`);
        }

        if (status)
        {
            status.textContent =
                "✓ Oda geçişi silindi.";
        }

        await loadHotspotList();

        const frame =
            document.getElementById(
                "tour360Frame");

        if (frame &&
            panoramaTour)
        {
            frame.src =
                `/virtual-tour/viewer.html?tourId=${panoramaTour.id}&v=${Date.now()}`;
        }
    }
    catch (error)
    {
        console.error(
            "Hotspot silme hatası:",
            error);

        if (status)
        {
            status.textContent =
                error.message;
        }
    }
}


/*
 * panoramaTour hazır olduktan sonra
 * hotspot listesini getir.
 */

async function waitAndLoadHotspotList()
{
    for (
        let attempt = 0;
        attempt < 15;
        attempt++)
    {
        if (panoramaTour &&
            panoramaTour.id)
        {
            await loadHotspotList();
            return;
        }

        await new Promise(
            resolve =>
                setTimeout(
                    resolve,
                    300));
    }
}


window.addEventListener(
    "load",
    () =>
    {
        waitAndLoadHotspotList();
    });



/*
 * ============================================================
 * HOTSPOT DUZENLEME
 * ============================================================
 */

let editingHotspotId = null;


/*
 * Viewer iframe'ini yeniler.
 */
function refreshHotspotViewer()
{
    if (!panoramaTour ||
        !panoramaTour.id)
    {
        return;
    }

    const frame =
        document.getElementById(
            "tour360Frame");

    if (!frame)
        return;

    frame.src =
        `/virtual-tour/viewer.html?tourId=${panoramaTour.id}&v=${Date.now()}`;
}


/*
 * Hotspot verilerini mevcut form alanlarına aktarır.
 */
function startHotspotEdit(
    hotspot,
    sourceSceneId)
{
    const source =
        document.getElementById(
            "hotspotSourceScene");

    const target =
        document.getElementById(
            "hotspotTargetScene");

    const title =
        document.getElementById(
            "hotspotTitle");

    const yaw =
        document.getElementById(
            "hotspotYaw");

    const pitch =
        document.getElementById(
            "hotspotPitch");

    const status =
        document.getElementById(
            "hotspotEditorStatus");

    const addButton =
        document.getElementById(
            "addHotspotButton");

    editingHotspotId =
        hotspot.id;

    if (source)
    {
        source.value =
            sourceSceneId || "";

        /*
         * Hotspot başka sahneye taşınmadığı için
         * kaynak sahneyi düzenleme sırasında kilitliyoruz.
         */
        source.disabled =
            true;
    }

    if (target)
    {
        target.value =
            hotspot.targetSceneId || "";
    }

    if (title)
    {
        title.value =
            hotspot.title || "";
    }

    if (yaw)
    {
        yaw.value =
            hotspot.yaw ?? 0;
    }

    if (pitch)
    {
        pitch.value =
            hotspot.pitch ?? 0;
    }

    if (addButton)
    {
        addButton.style.display =
            "none";
    }

    createHotspotEditButtons();

    if (status)
    {
        status.textContent =
            "Hotspot düzenleme modu.";
    }

    document.getElementById(
        "hotspotEditorPanel"
    )?.scrollIntoView(
    {
        behavior: "smooth",
        block: "start"
    });
}


/*
 * Kaydet / İptal butonlarını oluşturur.
 */
function createHotspotEditButtons()
{
    if (document.getElementById(
            "saveHotspotButton"))
    {
        return;
    }

    const addButton =
        document.getElementById(
            "addHotspotButton");

    if (!addButton ||
        !addButton.parentElement)
    {
        return;
    }

    const saveButton =
        document.createElement(
            "button");

    saveButton.id =
        "saveHotspotButton";

    saveButton.type =
        "button";

    saveButton.textContent =
        "Değişiklikleri Kaydet";

    saveButton.addEventListener(
        "click",
        saveHotspotEdit);


    const cancelButton =
        document.createElement(
            "button");

    cancelButton.id =
        "cancelHotspotButton";

    cancelButton.type =
        "button";

    cancelButton.textContent =
        "İptal";

    cancelButton.addEventListener(
        "click",
        cancelHotspotEdit);


    addButton.parentElement
        .appendChild(
            saveButton);

    addButton.parentElement
        .appendChild(
            cancelButton);
}


/*
 * Hotspot güncellemesini PUT endpoint'ine gönderir.
 */
async function saveHotspotEdit()
{
    if (!editingHotspotId)
        return;

    const target =
        document.getElementById(
            "hotspotTargetScene");

    const title =
        document.getElementById(
            "hotspotTitle");

    const yaw =
        document.getElementById(
            "hotspotYaw");

    const pitch =
        document.getElementById(
            "hotspotPitch");

    const status =
        document.getElementById(
            "hotspotEditorStatus");

    const saveButton =
        document.getElementById(
            "saveHotspotButton");


    const targetSceneId =
        target?.value || "";

    if (!targetSceneId)
    {
        if (status)
        {
            status.textContent =
                "Hedef oda seçin.";
        }

        return;
    }


    const targetName =
        target?.options[
            target.selectedIndex
        ]?.textContent || "Oda";


    const hotspotTitle =
        title?.value.trim() ||
        `${targetName} Git`;


    const request =
    {
        title:
            hotspotTitle,

        hotspotType:
            "Scene",

        targetSceneId:
            targetSceneId,

        yaw:
            Number(
                yaw?.value || 0),

        pitch:
            Number(
                pitch?.value || 0),

        description:
            targetName
    };


    try
    {
        if (saveButton)
        {
            saveButton.disabled =
                true;
        }

        if (status)
        {
            status.textContent =
                "Hotspot güncelleniyor...";
        }


        const response =
            await fetch(
                `/api/properties/virtual-tour-hotspots/${editingHotspotId}`,
                {
                    method:
                        "PUT",

                    headers:
                    {
                        "Content-Type":
                            "application/json"
                    },

                    body:
                        JSON.stringify(
                            request)
                });


        const body =
            await response.text();


        if (!response.ok)
        {
            throw new Error(
                `Hotspot güncellenemedi: HTTP ${response.status} ${body}`
            );
        }


        console.log(
            "Hotspot güncellendi:",
            body);


        if (status)
        {
            status.textContent =
                "✓ Hotspot güncellendi.";
        }


        cancelHotspotEdit(
            false);


        await loadHotspotList();

        refreshHotspotViewer();
    }

    catch (error)
    {
        console.error(
            "Hotspot güncelleme hatası:",
            error);

        if (status)
        {
            status.textContent =
                error.message;
        }
    }

    finally
    {
        if (saveButton)
        {
            saveButton.disabled =
                false;
        }
    }
}


/*
 * Düzenleme modundan çıkar.
 */
function cancelHotspotEdit(
    showMessage = true)
{
    editingHotspotId =
        null;


    const source =
        document.getElementById(
            "hotspotSourceScene");

    const target =
        document.getElementById(
            "hotspotTargetScene");

    const title =
        document.getElementById(
            "hotspotTitle");

    const yaw =
        document.getElementById(
            "hotspotYaw");

    const pitch =
        document.getElementById(
            "hotspotPitch");

    const status =
        document.getElementById(
            "hotspotEditorStatus");

    const addButton =
        document.getElementById(
            "addHotspotButton");


    if (source)
    {
        source.disabled =
            false;

        source.value =
            "";
    }


    if (target)
    {
        target.value =
            "";
    }


    if (title)
    {
        title.value =
            "";
    }


    if (yaw)
    {
        yaw.value =
            "0";
    }


    if (pitch)
    {
        pitch.value =
            "0";
    }


    document.getElementById(
        "saveHotspotButton"
    )?.remove();


    document.getElementById(
        "cancelHotspotButton"
    )?.remove();


    if (addButton)
    {
        addButton.style.display =
            "";
    }


    if (showMessage &&
        status)
    {
        status.textContent =
            "Düzenleme iptal edildi.";
    }
}


/*
 * Mevcut loadHotspotList fonksiyonunu sakla.
 *
 * Sonra listeye Düzenle butonlarını ekleyen
 * yeni sürümle sarıyoruz.
 */
const originalLoadHotspotList =
    loadHotspotList;


loadHotspotList =
    async function()
    {
        const container =
            document.getElementById(
                "hotspotList");

        if (!container)
            return;


        if (!panoramaTour ||
            !panoramaTour.id)
        {
            container.textContent =
                "Aktif 360° tur bulunamadı.";

            return;
        }


        container.textContent =
            "Geçişler yükleniyor...";


        try
        {
            const response =
                await fetch(
                    `/api/properties/virtual-tours/${panoramaTour.id}/viewer?v=${Date.now()}`,
                    {
                        cache:
                            "no-store"
                    });


            if (!response.ok)
            {
                throw new Error(
                    `Viewer API HTTP ${response.status}`
                );
            }


            const data =
                await response.json();


            const scenes =
                Array.isArray(
                    data.scenes)
                    ? data.scenes
                    : [];


            container.innerHTML =
                "";


            let hotspotCount =
                0;


            for (const scene of scenes)
            {
                const hotspots =
                    Array.isArray(
                        scene.hotspots)
                        ? scene.hotspots
                        : [];


                for (const hotspot of hotspots)
                {
                    hotspotCount++;


                    const targetScene =
                        scenes.find(
                            x =>
                                String(
                                    x.id
                                ).toLowerCase() ===
                                String(
                                    hotspot.targetSceneId || ""
                                ).toLowerCase()
                        );


                    const sourceName =
                        scene.name ||
                        "Bilinmeyen oda";


                    const targetName =
                        targetScene?.name ||
                        "Hedef yok";


                    const row =
                        document.createElement(
                            "div");


                    row.style.display =
                        "flex";

                    row.style.alignItems =
                        "center";

                    row.style.justifyContent =
                        "space-between";

                    row.style.gap =
                        "12px";

                    row.style.padding =
                        "10px";

                    row.style.margin =
                        "8px 0";

                    row.style.border =
                        "1px solid #ddd";

                    row.style.borderRadius =
                        "8px";


                    const text =
                        document.createElement(
                            "span");


                    text.textContent =
                        `${sourceName} → ${targetName} — ${hotspot.title || "Geçiş"} | Yaw: ${hotspot.yaw ?? 0}, Pitch: ${hotspot.pitch ?? 0}`;


                    const actions =
                        document.createElement(
                            "div");


                    actions.style.display =
                        "flex";

                    actions.style.gap =
                        "8px";


                    const editButton =
                        document.createElement(
                            "button");


                    editButton.type =
                        "button";

                    editButton.textContent =
                        "Düzenle";


                    editButton.addEventListener(
                        "click",
                        () =>
                        {
                            startHotspotEdit(
                                hotspot,
                                scene.id);
                        });


                    const deleteButton =
                        document.createElement(
                            "button");


                    deleteButton.type =
                        "button";

                    deleteButton.textContent =
                        "Sil";


                    deleteButton.addEventListener(
                        "click",
                        async () =>
                        {
                            await deleteHotspot(
                                hotspot.id,
                                sourceName,
                                targetName);
                        });


                    actions.appendChild(
                        editButton);

                    actions.appendChild(
                        deleteButton);


                    row.appendChild(
                        text);

                    row.appendChild(
                        actions);


                    container.appendChild(
                        row);
                }
            }


            if (hotspotCount === 0)
            {
                container.textContent =
                    "Bu turda oda geçişi bulunmuyor.";
            }


            console.log(
                "Hotspot yönetim listesi yüklendi:",
                hotspotCount);
        }

        catch (error)
        {
            console.error(
                "Hotspot listesi yüklenemedi:",
                error);

            container.textContent =
                "Geçişler yüklenemedi: " +
                error.message;
        }
    };


console.log(
    "Hotspot düzenleme sistemi aktif.");
