window.panorama360 = {

    init: function () {

        const element =
            document.getElementById("panorama-360");

        if (!element) {
            return;
        }

        if (element.dataset.initialized === "true") {
            return;
        }

        element.dataset.initialized = "true";

        const image =
            element.querySelector(".panorama360-image");

        if (!image) {
            return;
        }

        let dragging = false;
        let lastX = 0;
        let offsetX = 0;
        let zoom = 1;

        const minZoom = 1;
        const maxZoom = 2.5;

        function updateImage() {

            image.style.transform =
                "translateX(" +
                offsetX +
                "px) scale(" +
                zoom +
                ")";

        }

        function move(delta) {

            offsetX += delta;

            const limit =
                element.clientWidth * 0.75;

            if (offsetX > limit) {
                offsetX = limit;
            }

            if (offsetX < -limit) {
                offsetX = -limit;
            }

            updateImage();
        }

        element.addEventListener(
            "mousedown",
            function (e) {

                dragging = true;
                lastX = e.clientX;

            }
        );

        window.addEventListener(
            "mouseup",
            function () {

                dragging = false;

            }
        );

        window.addEventListener(
            "mousemove",
            function (e) {

                if (!dragging) {
                    return;
                }

                const difference =
                    e.clientX - lastX;

                move(difference);

                lastX = e.clientX;

            }
        );

        element.addEventListener(
            "touchstart",
            function (e) {

                if (!e.touches.length) {
                    return;
                }

                dragging = true;
                lastX = e.touches[0].clientX;

            },
            { passive: true }
        );

        element.addEventListener(
            "touchend",
            function () {

                dragging = false;

            },
            { passive: true }
        );

        element.addEventListener(
            "touchmove",
            function (e) {

                if (!dragging || !e.touches.length) {
                    return;
                }

                const currentX =
                    e.touches[0].clientX;

                const difference =
                    currentX - lastX;

                move(difference);

                lastX = currentX;

            },
            { passive: true }
        );

        const zoomIn =
            element.querySelector(
                '[data-action="zoom-in"]'
            );

        const zoomOut =
            element.querySelector(
                '[data-action="zoom-out"]'
            );

        const reset =
            element.querySelector(
                '[data-action="reset"]'
            );

        const fullscreen =
            element.querySelector(
                '[data-action="fullscreen"]'
            );

        if (zoomIn) {

            zoomIn.addEventListener(
                "click",
                function (e) {

                    e.stopPropagation();

                    zoom =
                        Math.min(
                            maxZoom,
                            zoom + 0.25
                        );

                    updateImage();

                }
            );

        }

        if (zoomOut) {

            zoomOut.addEventListener(
                "click",
                function (e) {

                    e.stopPropagation();

                    zoom =
                        Math.max(
                            minZoom,
                            zoom - 0.25
                        );

                    updateImage();

                }
            );

        }

        if (reset) {

            reset.addEventListener(
                "click",
                function (e) {

                    e.stopPropagation();

                    offsetX = 0;
                    zoom = 1;

                    updateImage();

                }
            );

        }

        if (fullscreen) {

            fullscreen.addEventListener(
                "click",
                async function (e) {

                    e.stopPropagation();

                    try {

                        if (!document.fullscreenElement) {

                            await element.requestFullscreen();

                        }
                        else {

                            await document.exitFullscreen();

                        }

                    }
                    catch (error) {

                        console.error(
                            "360 fullscreen error:",
                            error
                        );

                    }

                }
            );

        }

        element.addEventListener(
            "wheel",
            function (e) {

                e.preventDefault();

                if (e.deltaY < 0) {

                    zoom =
                        Math.min(
                            maxZoom,
                            zoom + 0.1
                        );

                }
                else {

                    zoom =
                        Math.max(
                            minZoom,
                            zoom - 0.1
                        );

                }

                updateImage();

            },
            { passive: false }
        );

        updateImage();

    }
};
