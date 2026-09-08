import * as THREE
    from "three";

import {
    OrbitControls
}
from "three/addons/controls/OrbitControls.js";

import {
    GLTFLoader
}
from "three/addons/loaders/GLTFLoader.js";


const canvas =
    document.getElementById(
        "modelCanvas");

const loading =
    document.getElementById(
        "loading");

const tourTitle =
    document.getElementById(
        "tourTitle");

const modelTitle =
    document.getElementById(
        "modelTitle");

const modelList =
    document.getElementById(
        "modelList");

const autoRotateButton =
    document.getElementById(
        "autoRotate");


const renderer =
    new THREE.WebGLRenderer({
        canvas,
        antialias: true,
        alpha: false
    });

renderer.setPixelRatio(
    Math.min(
        window.devicePixelRatio || 1,
        2));

renderer.shadowMap.enabled =
    true;

renderer.outputColorSpace =
    THREE.SRGBColorSpace;


const scene =
    new THREE.Scene();

scene.background =
    new THREE.Color(
        0xeeeeee);


const camera =
    new THREE.PerspectiveCamera(
        45,
        1,
        0.01,
        10000);


camera.position.set(
    4,
    3,
    6);


const controls =
    new OrbitControls(
        camera,
        renderer.domElement);

controls.enableDamping =
    true;

controls.dampingFactor =
    0.06;

controls.enablePan =
    true;

controls.enableZoom =
    true;

controls.enableRotate =
    true;

controls.minDistance =
    0.25;

controls.maxDistance =
    500;


const ambientLight =
    new THREE.HemisphereLight(
        0xffffff,
        0x444444,
        2.5);

scene.add(
    ambientLight);


const directionalLight =
    new THREE.DirectionalLight(
        0xffffff,
        3);

directionalLight.position.set(
    5,
    10,
    7);

directionalLight.castShadow =
    true;

scene.add(
    directionalLight);


const secondLight =
    new THREE.DirectionalLight(
        0xffffff,
        1.5);

secondLight.position.set(
    -5,
    3,
    -5);

scene.add(
    secondLight);


const loader =
    new GLTFLoader();


let tour = null;

let currentModel =
    null;

let currentModelData =
    null;


function resize()
{
    const width =
        canvas.clientWidth;

    const height =
        canvas.clientHeight;

    const pixelRatio =
        renderer.getPixelRatio();

    const requiredWidth =
        Math.floor(
            width * pixelRatio);

    const requiredHeight =
        Math.floor(
            height * pixelRatio);

    if (
        canvas.width !==
            requiredWidth ||
        canvas.height !==
            requiredHeight
    )
    {
        renderer.setSize(
            width,
            height,
            false);

        camera.aspect =
            width /
            Math.max(
                height,
                1);

        camera.updateProjectionMatrix();
    }
}


function clearCurrentModel()
{
    if (!currentModel)
        return;

    scene.remove(
        currentModel);

    currentModel.traverse(
        object =>
        {
            if (object.geometry)
            {
                object.geometry.dispose();
            }

            if (object.material)
            {
                const materials =
                    Array.isArray(
                        object.material)
                        ? object.material
                        : [object.material];

                for (
                    const material
                    of materials
                )
                {
                    for (
                        const value
                        of Object.values(
                            material)
                    )
                    {
                        if (
                            value &&
                            value.isTexture
                        )
                        {
                            value.dispose();
                        }
                    }

                    material.dispose();
                }
            }
        });

    currentModel =
        null;
}


function fitCameraToModel(
    object)
{
    const box =
        new THREE.Box3()
            .setFromObject(
                object);

    if (box.isEmpty())
        return;

    const size =
        box.getSize(
            new THREE.Vector3());

    const center =
        box.getCenter(
            new THREE.Vector3());

    const maxDimension =
        Math.max(
            size.x,
            size.y,
            size.z);

    const fov =
        camera.fov *
        Math.PI /
        180;

    let distance =
        maxDimension /
        (
            2 *
            Math.tan(
                fov / 2)
        );

    distance *= 1.6;

    camera.position.set(
        center.x +
            distance,
        center.y +
            distance * 0.45,
        center.z +
            distance);

    camera.near =
        Math.max(
            distance / 1000,
            0.01);

    camera.far =
        Math.max(
            distance * 100,
            1000);

    camera.updateProjectionMatrix();

    controls.target.copy(
        center);

    controls.minDistance =
        Math.max(
            maxDimension * 0.15,
            0.1);

    controls.maxDistance =
        Math.max(
            maxDimension * 20,
            20);

    controls.update();
}


function configureMeshes(
    root)
{
    root.traverse(
        object =>
        {
            if (!object.isMesh)
                return;

            object.castShadow =
                true;

            object.receiveShadow =
                true;
        });
}


async function loadModel(
    modelData)
{
    loading.style.display =
        "block";

    loading.textContent =
        "3D model yükleniyor...";

    clearCurrentModel();

    currentModelData =
        modelData;

    controls.enableRotate =
        modelData.allowRotation !==
            false;

    controls.enableZoom =
        modelData.allowZoom !==
            false;

    controls.autoRotate =
        modelData.autoRotate ===
            true;

    controls.autoRotateSpeed =
        Number(
            modelData.autoRotateSpeed ||
            1);

    updateAutoRotateButton();

    try
    {
        const gltf =
            await loader.loadAsync(
                modelData.modelUrl);

        currentModel =
            gltf.scene;

        configureMeshes(
            currentModel);

        scene.add(
            currentModel);

        fitCameraToModel(
            currentModel);

        modelTitle.textContent =
            modelData.name;

        updateModelButtons();

        loading.style.display =
            "none";
    }
    catch (error)
    {
        console.error(
            error);

        loading.textContent =
            "3D model yüklenemedi.";
    }
}


function createModelButtons()
{
    modelList.innerHTML =
        "";

    for (
        const model
        of tour.models
    )
    {
        const button =
            document.createElement(
                "button");

        button.className =
            "model-button";

        button.dataset.modelId =
            model.id;

        button.textContent =
            model.name;

        button.addEventListener(
            "click",
            () =>
                loadModel(
                    model));

        modelList.appendChild(
            button);
    }
}


function updateModelButtons()
{
    document
        .querySelectorAll(
            ".model-button")
        .forEach(
            button =>
            {
                button.classList.toggle(
                    "active",
                    button.dataset.modelId ===
                        currentModelData?.id);
            });
}


function updateAutoRotateButton()
{
    autoRotateButton
        .classList
        .toggle(
            "active",
            controls.autoRotate);

    autoRotateButton.textContent =
        controls.autoRotate
            ? "360° ✓"
            : "360°";
}


document
    .getElementById(
        "zoomIn")
    .addEventListener(
        "click",
        () =>
        {
            if (
                !controls.enableZoom
            )
                return;

            const direction =
                new THREE.Vector3();

            direction
                .subVectors(
                    camera.position,
                    controls.target);

            direction.multiplyScalar(
                0.82);

            camera.position.copy(
                controls.target)
                .add(
                    direction);

            controls.update();
        });


document
    .getElementById(
        "zoomOut")
    .addEventListener(
        "click",
        () =>
        {
            if (
                !controls.enableZoom
            )
                return;

            const direction =
                new THREE.Vector3();

            direction
                .subVectors(
                    camera.position,
                    controls.target);

            direction.multiplyScalar(
                1.22);

            camera.position.copy(
                controls.target)
                .add(
                    direction);

            controls.update();
        });


document
    .getElementById(
        "resetView")
    .addEventListener(
        "click",
        () =>
        {
            if (currentModel)
            {
                fitCameraToModel(
                    currentModel);
            }
        });


autoRotateButton
    .addEventListener(
        "click",
        () =>
        {
            if (
                currentModelData &&
                currentModelData.allowRotation ===
                    false
            )
            {
                return;
            }

            controls.autoRotate =
                !controls.autoRotate;

            updateAutoRotateButton();
        });


document
    .getElementById(
        "fullscreenButton")
    .addEventListener(
        "click",
        async () =>
        {
            const viewer =
                document.getElementById(
                    "viewer");

            if (
                !document.fullscreenElement
            )
            {
                await viewer
                    .requestFullscreen();
            }
            else
            {
                await document
                    .exitFullscreen();
            }
        });


async function initialize()
{
    const parameters =
        new URLSearchParams(
            window.location.search);

    const tourId =
        parameters.get(
            "tourId");

    const requestedModelId =
        parameters.get(
            "modelId");

    if (!tourId)
    {
        loading.textContent =
            "tourId belirtilmedi.";

        return;
    }

    try
    {
        const response =
            await fetch(
                `/api/properties/virtual-tours/${tourId}/viewer`);

        if (!response.ok)
        {
            throw new Error(
                "Virtual tour bulunamadı.");
        }

        tour =
            await response.json();

        tourTitle.textContent =
            tour.title ||
            "3D Emlak Görünümü";

        if (
            !tour.models ||
            tour.models.length === 0
        )
        {
            loading.textContent =
                "Bu ilanda 3D model bulunmuyor.";

            return;
        }

        createModelButtons();

        let model =
            null;

        if (requestedModelId)
        {
            model =
                tour.models.find(
                    x =>
                        x.id ===
                        requestedModelId);
        }

        model =
            model ||
            tour.models[0];

        await loadModel(
            model);
    }
    catch (error)
    {
        console.error(
            error);

        loading.textContent =
            error.message;
    }
}


function animate()
{
    requestAnimationFrame(
        animate);

    resize();

    controls.update();

    renderer.render(
        scene,
        camera);
}


animate();
initialize();
