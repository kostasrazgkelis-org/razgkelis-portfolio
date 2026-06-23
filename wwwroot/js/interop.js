// ============================================================
//  Portfolio interop — scroll reveals, nav state, hero particles
//  Kept intentionally small; Blazor owns structure & state.
// ============================================================
window.portfolio = (function () {
    const reduceMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    let revealObserver = null;

    // ---- Scroll reveals ---------------------------------------------------
    function ensureObserver() {
        if (revealObserver || reduceMotion) return;
        revealObserver = new IntersectionObserver((entries) => {
            for (const entry of entries) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("is-visible");
                    revealObserver.unobserve(entry.target);
                }
            }
        }, { threshold: 0.12, rootMargin: "0px 0px -8% 0px" });
    }

    function observeReveals() {
        if (reduceMotion) {
            document.querySelectorAll("[data-reveal]").forEach(el => el.classList.add("is-visible"));
            return;
        }
        ensureObserver();
        // slight delay so freshly-rendered Blazor nodes are in the DOM
        requestAnimationFrame(() => {
            document.querySelectorAll("[data-reveal]:not(.is-visible)").forEach(el => revealObserver.observe(el));
        });
    }

    // ---- Sticky nav background on scroll ----------------------------------
    function initNav() {
        const nav = document.querySelector(".nav");
        if (!nav) return;
        const onScroll = () => nav.classList.toggle("nav--scrolled", window.scrollY > 24);
        onScroll();
        window.removeEventListener("scroll", nav.__navHandler || (() => {}));
        nav.__navHandler = onScroll;
        window.addEventListener("scroll", onScroll, { passive: true });
    }

    function scrollTop() {
        window.scrollTo({ top: 0, behavior: reduceMotion ? "auto" : "smooth" });
    }

    // ---- Hero particle / constellation backdrop ---------------------------
    function initHero(canvas) {
        if (!canvas || reduceMotion) return;
        const ctx = canvas.getContext("2d");
        let w, h, dpr, points = [], raf = null;
        const COLOR = "#fca311";

        function resize() {
            dpr = Math.min(window.devicePixelRatio || 1, 2);
            w = canvas.clientWidth; h = canvas.clientHeight;
            canvas.width = w * dpr; canvas.height = h * dpr;
            ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
            const count = Math.min(90, Math.floor((w * h) / 16000));
            points = Array.from({ length: count }, () => ({
                x: Math.random() * w,
                y: Math.random() * h,
                vx: (Math.random() - 0.5) * 0.25,
                vy: (Math.random() - 0.5) * 0.25,
            }));
        }

        function frame() {
            ctx.clearRect(0, 0, w, h);
            for (const p of points) {
                p.x += p.vx; p.y += p.vy;
                if (p.x < 0 || p.x > w) p.vx *= -1;
                if (p.y < 0 || p.y > h) p.vy *= -1;
            }
            // links
            for (let i = 0; i < points.length; i++) {
                for (let j = i + 1; j < points.length; j++) {
                    const a = points[i], b = points[j];
                    const dx = a.x - b.x, dy = a.y - b.y;
                    const dist = Math.hypot(dx, dy);
                    if (dist < 130) {
                        ctx.globalAlpha = (1 - dist / 130) * 0.22;
                        ctx.strokeStyle = COLOR;
                        ctx.lineWidth = 1;
                        ctx.beginPath();
                        ctx.moveTo(a.x, a.y); ctx.lineTo(b.x, b.y); ctx.stroke();
                    }
                }
            }
            // dots
            ctx.globalAlpha = 0.7;
            ctx.fillStyle = COLOR;
            for (const p of points) {
                ctx.beginPath(); ctx.arc(p.x, p.y, 1.4, 0, Math.PI * 2); ctx.fill();
            }
            ctx.globalAlpha = 1;
            raf = requestAnimationFrame(frame);
        }

        // tidy up a previous instance if hero re-rendered
        if (canvas.__cleanup) canvas.__cleanup();
        const onResize = () => resize();
        window.addEventListener("resize", onResize);
        canvas.__cleanup = () => { cancelAnimationFrame(raf); window.removeEventListener("resize", onResize); };

        resize();
        frame();
    }

    return { observeReveals, initNav, initHero, scrollTop, reduceMotion };
})();
