namespace MyWebApp.Data;

// ============================================================
//  Models
// ============================================================

public record Project(
    string Slug,
    string Title,
    string Tagline,
    string Problem,
    string Role,
    string TeamSize,
    string[] TechStack,
    string[] Narrative,
    bool Featured = false);

public record SkillGroup(string Name, string Caption, string[] Items);

public record TimelineEntry(string Period, string Title, string Org, string Summary);

public record EducationEntry(string Period, string Degree, string Institution, string Detail);

public record PersonalFacet(string Icon, string Title, string Body, string[] Tags,
    string? Link = null, string? LinkLabel = null);

public record FocusArea(string Index, string Title, string Body);

public record StatItem(string Number, string Label);

// ============================================================
//  Site-wide config — swap the placeholder links once provided
// ============================================================

public static class Site
{
    public const string FullName = "Konstantinos A. Razgkelis";
    public const string ShortName = "Konstantinos Razgkelis";
    public const string Role = "Senior Software Engineer";
    public const string Email = "k.razgkelis@outlook.com";

    public const string LinkedInUrl = "https://www.linkedin.com/in/kostasrazgkelis/";
    public const string PublicationUrl = "https://link.springer.com/chapter/10.1007/978-3-031-26507-5_37";
    public const string VolleyballUrl = "https://volleybox.net/el/konstantinos-razgkelis-p215168";
    public const string CvPath = "cv/Konstantinos-Razgkelis-CV.pdf"; // replace the PDF in wwwroot/cv/ to update

    public const string Location = "Thessaloniki, Greece";
    public const string Company = "Onelity";
}

// ============================================================
//  Content (sourced & rewritten from the CV)
// ============================================================

public static class PortfolioData
{
    // ---- Dynamic experience duration ------------------------------------
    // Career started January 2022; the displayed duration updates over time.
    public static readonly DateTime CareerStart = new(2022, 1, 1);

    public static double ExperienceYears => (DateTime.UtcNow - CareerStart).TotalDays / 365.2425;

    // Rounding rule based on the fraction past the last whole year (`whole`),
    // where `next` = whole + 1:
    //   0.0 – 0.2  -> "{whole}+ years"   (just over a whole year)
    //   0.2 – 0.4  -> "{whole}.5 years"  (approaching the half year)
    //   0.4 – 0.6  -> "{whole}.5+ years" (just over the half year)
    //   0.6 – 1.0  -> "~{next} years"    (approaching the next whole year)
    public static string ExperiencePhrase => BuildExperience(longForm: true);

    // Compact form for the stat tile ("4+" / "4.5" / "4.5+" / "~5").
    public static string ExperienceShort => BuildExperience(longForm: false);

    private static string BuildExperience(bool longForm)
    {
        var years = ExperienceYears;
        var whole = (int)Math.Floor(years);
        var next = whole + 1;
        var frac = years - whole;

        var number = frac < 0.2 ? $"{whole}+"
                   : frac < 0.4 ? $"{whole}.5"
                   : frac < 0.6 ? $"{whole}.5+"
                   : $"~{next}";

        return longForm ? $"{number} years" : number;
    }

    public static string IntroLong =>
        $"I'm a software engineer with {ExperiencePhrase} of experience that runs across backend, mobile, " +
        "frontend and test automation. I've helped break monoliths apart into microservices, shipped web and " +
        "mobile products end to end, designed APIs and data models, and leaned hard on CI/CD so the code I " +
        "deliver is well-tested and trustworthy. These days my focus is cloud-based platforms, modern backends, " +
        "distributed services, orchestration and automated delivery.";

    public const string IntroShort =
        "Backend, mobile, frontend and test automation — I build distributed systems and ship them through CI/CD.";

    public static readonly string[] TypedPhrases =
    {
        "Backend systems & APIs",
        "Cross-platform mobile apps",
        "Monolith → microservices",
        "Test automation at scale",
        "AI-driven data quality",
        "Cloud, Docker & Kubernetes",
    };

    public static StatItem[] Stats =>
    new[]
    {
        new StatItem(ExperienceShort, "Years building"),
        new StatItem("7", "Projects shipped"),
        new StatItem("9+", "Core technologies"),
        new StatItem("1", "Published paper"),
    };

    public static readonly FocusArea[] FocusAreas =
    {
        new("01", "Backend & APIs",
            "I design data models and APIs and decouple monoliths into microservices with reliability, security and scalability in mind — in .NET Core, Spring Boot and Django."),
        new("02", "Mobile & Frontend",
            "I build cross-platform mobile apps with .NET MAUI and web frontends with React, owning features from the service layer all the way to the screen."),
        new("03", "Test Automation & QA",
            "I replace manual testing with automation frameworks — Robot Framework and Playwright — and I'm ISTQB certified."),
        new("04", "Cloud & Delivery",
            "I containerize and orchestrate with Docker and Kubernetes and wire up CI/CD on Azure and GitLab so delivery stays fast and repeatable."),
    };

    public static readonly SkillGroup[] SkillGroups =
    {
        new("Languages", "Day-to-day", new[] { "C#", "Java", "Python" }),
        new("Frameworks", "Backend & web", new[] { ".NET Core", "Spring", "Django", ".NET MAUI", "React" }),
        new("Databases", "Persistence", new[] { "PostgreSQL", "MySQL" }),
        new("Orchestration", "Run anywhere", new[] { "Docker", "Kubernetes", "Celery"}),
        new("CI / CD", "Automated delivery", new[] { "Azure", "GitLab", "Jenkins" }),
        new("QA & Testing", "Quality gates", new[] { "Robot Framework", "Playwright" }),
        new("Data Processing", "At scale", new[] { "Apache Spark" }),
    };

    public static readonly EducationEntry[] Education =
    {
        new("2022 — 2025", "MSc, Data & Web Science",
            "Aristotle University of Thessaloniki",
            "School of Informatics."),
        new("2017 — 2022", "BSc, Applied Informatics",
            "University of Macedonia",
            "Department of Applied Informatics."),
    };

    public static readonly (string Name, string Level)[] Languages =
    {
        ("Greek", "Native"),
        ("English", "C2 — Proficient"),
    };

    public static readonly TimelineEntry Experience =
        new("01 / 2022 — Present", "Senior Software Engineer", Site.Company,
            "My home base for everything below — from refactoring government systems and building cross-platform " +
            "mobile products to standing up test-automation frameworks and AI-driven data-quality services, " +
            "mostly in team settings ranging from solo work to international teams of ten.");

    public static readonly PersonalFacet[] Personal =
    {
        new("\U0001F3D0", "Volleyball",
            "I've played volleyball for years — from academy teams to university and official league matches, " +
            "in Greece and abroad. It genuinely shaped me: it taught me discipline, real teamwork, and how to stay " +
            "calm when the pressure is on.",
            new[] { "Discipline", "Teamwork", "Composure" },
            Link: Site.VolleyballUrl, LinkLabel: "Volleybox profile"),
        new("\U0001F9ED", "Travel",
            "I love traveling and exploring new places. New environments keep me curious and adaptable.",
            new[] { "Curiosity", "Adaptability" }),
        new("⛵", "Skipper Certification",
            "I earned my skipper certification because I enjoy being out at sea and taking on new adventures. " +
            "Reading conditions and making the call as the person responsible is leadership and calm under pressure, " +
            "distilled.",
            new[] { "Leadership", "Responsibility", "Calm under pressure" }),
    };

    public static readonly Project[] Projects =
    {
        new(
            "government-access-management",
            "Government Access Management",
            "Secure access & authorization for a government platform",
            "The Dutch Government's authorization and card-management platform had accumulated significant legacy " +
            "complexity. It required modernization into a maintainable, scalable architecture while preserving the " +
            "strict security guarantees expected of a national access-control system.",
            "Backend Developer",
            "Team environment",
            new[] { ".NET Core", "IIS", "AWS" },
            new[]
            {
                "I led the refactoring and deployment of the applications responsible for the platform's secure access and authorization processes.",
                "I decomposed the monolith into a set of well-defined microservices, resulting in an architecture of more than 50 services that coordinate asynchronous workloads.",
                "Throughout the engagement I treated reliability, security and scalability as first-class concerns, and modernized the build, integration and deployment pipelines to support continuous, dependable releases.",
            },
            Featured: true),

        new(
            "elevator-operations",
            "Mobile Elevator Operations",
            "Optimizing elevator usage at the World Trade Center",
            "Elevator operations at the World Trade Center needed to be optimized. The client required a cross-platform " +
            "application, supported by robust backend services, to coordinate usage and deliver real-time information to operational staff.",
            "Backend & Mobile Developer",
            "Team of 6",
            new[] { ".NET Core", ".NET MAUI" },
            new[]
            {
                "I developed the backend services and contributed key mobile features for a cross-platform application designed to optimize elevator usage at the World Trade Center.",
                "Working within a team of six, I delivered across both the service layer and the .NET MAUI client, ensuring operational data reached the staff managing real-time tasks accurately and on time.",
            },
            Featured: true),

        new(
            "e-commerce",
            "Mobile e-Commerce",
            "Mobile e-Commerce store for a power-tools manufacturer",
            "A manufacturer of power tools and garden equipment wanted to engage customers directly through a polished, " +
            "reliable mobile commerce experience rather than relying solely on third-party channels.",
            "Backend & Mobile Developer",
            "Team of 4",
            new[] { ".NET Core", ".NET MAUI" },
            new[]
            {
                "I designed and delivered a mobile e-commerce application for a manufacturer of power tools and garden equipment.",
                "As a backend and mobile developer in a team of four, I owned both the services powering the product catalogue and the customer-facing .NET MAUI application.",
            },
            Featured: true),

        new(
            "techsaloniki",
            "TechSaloniki Web Platform",
            "The digital home of a major tech event",
            "TechSaloniki, a major technology event, required a dependable digital platform to support its operations " +
            "and audience — to be delivered and maintained by a single developer.",
            "Sole Software Developer",
            "Solo",
            new[] { ".NET Core", "React" },
            new[]
            {
                "I developed and maintained the digital platform for the TechSaloniki technology event.",
                "Operating as the sole developer, I owned the full stack end to end — from the .NET Core backend to the React frontend — together with its ongoing maintenance.",
            }),

        new(
            "crm",
            "CRM Web Application",
            "An enterprise tool for internal operations",
            "An organization needed a centralized enterprise management tool to support and streamline its internal operations across teams.",
            "Software Developer",
            "Team of 10",
            new[] { ".NET Core", "React" },
            new[]
            {
                "I contributed to the development of an enterprise CRM that supports the organization's internal operations.",
                "As part of a team of ten, I worked across the .NET Core backend and React frontend of a system used to run day-to-day business processes.",
            }),

        new(
            "test-automation",
            "Test Automation Framework",
            "Replacing manual SAP S/4HANA testing",
            "An automotive supplier validated its SAP S/4HANA transactions manually — a process that was slow, " +
            "repetitive and increasingly difficult to trust as coverage requirements grew.",
            "Test Automation Developer",
            "Scaled to a team of 4",
            new[] { "Playwright", "Robot Framework" },
            new[]
            {
                "I initiated an automation framework to replace manual transaction testing for SAP S/4HANA systems at an automotive supplier.",
                "The initiative scaled into a team of four developers, with the framework progressively assuming the repetitive transaction checks that had previously been performed manually.",
            }),

        new(
            "ai-platform",
            "AI-Powered Data-Quality Platform",
            "Improving master data with AI-driven models",
            "Poor master-data quality was a persistent operational problem. The objective was a web platform that " +
            "leverages AI-driven models to detect, clean and improve master data at scale.",
            "Backend Developer",
            "International team of 7",
            new[] { "Django", "React", "Docker", "Jenkins", "Celery" },
            new[]
            {
                "I built and maintained a web application focused on improving master-data quality through AI-driven models.",
                "As a backend developer in an international team of seven, I implemented the processing pipeline with Django and Celery, integrated the React frontend, and used Docker and Jenkins to containerize and deliver the platform.",
            }),
    };

    public static Project? FindProject(string slug) =>
        Array.Find(Projects, p => p.Slug == slug);

    public static IEnumerable<Project> FeaturedProjects =>
        Projects.Where(p => p.Featured);
}
