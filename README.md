# Konstantinos A. Razgkelis — Personal Portfolio

A cinematic, dark-themed personal portfolio built with **Blazor WebAssembly (.NET 10)**.
Multi-page, fully client-side, and statically hostable for free.

## Run locally

```bash
dotnet run
```

Then open the URL printed in the console (e.g. `http://localhost:5174`).

## Build for deployment (static files)

```bash
dotnet publish -c Release
```

The deployable static site is produced under `bin/Release/net10.0/publish/wwwroot`.
Host it on GitHub Pages, Netlify, Azure Static Web Apps, etc. (For sub-path hosting
like GitHub Pages, set `<base href>` in `wwwroot/index.html` accordingly.)

## Structure

| Path | Purpose |
|------|---------|
| `Pages/` | Routed pages — Home, About, Experience, Projects, ProjectDetail, Skills, Personal, Contact |
| `Components/` | Reusable UI — Hero pieces, TypedText, ParticleBackdrop, cards, badges |
| `Layout/` | MainLayout, NavMenu (sticky + mobile), Footer |
| `Data/PortfolioData.cs` | **All content + site config** lives here |
| `wwwroot/css/app.css` | Full design system (colors, type, motion) |
| `wwwroot/js/interop.js` | Scroll reveals, nav state, hero particle canvas |

## Personalizing content

All content and links live in `Data/PortfolioData.cs`. The `Site` class holds the
real links (LinkedIn, Springer publication, Volleybox profile, CV path). Everything
else (projects, skills, experience, education, personal) is in the same file — edit
it there and the whole site updates automatically.

> GitHub link is intentionally omitted. To add it later, restore a `GitHubUrl`
> constant in `Site` and a matching `<a>` in `Components/SocialLinks.razor`.

## Design

- **Palette:** black / prussian-blue base, `#fca311` orange accent, alabaster text
- **Type:** *Big Shoulders Stencil Text* (headings) + Inter (body) + JetBrains Mono (labels)
- **Motion:** scroll reveals, hero particle field, typed tagline, route transitions —
  all disabled automatically under `prefers-reduced-motion`
- **Theme:** dark only
