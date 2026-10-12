using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON ===\n");

var db = new DatabaseService();
var ui = new UiService();

db.Config.Theme = "Dark";

Console.WriteLine("Admin changed theme to Dark.");
ui.Render();

Console.WriteLine(
    $"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");

Console.WriteLine(
    $"Times config was loaded from disk: {AppConfig.LoadCount}");


Console.WriteLine("\n=== PROTOTYPE ===\n");

var sw = Stopwatch.StartNew();

var army = new List<Enemy>();

Enemy prototype = new Orc();
prototype.Name = "Orc-1";

army.Add(prototype);

for (int i = 2; i <= 5; i++)
{
    Enemy orc = prototype.Clone();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}

sw.Stop();

Console.WriteLine($"Created 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";

Enemy copy = original.Clone();

Console.WriteLine($"Original model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;

Console.WriteLine("\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is now: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");

Console.WriteLine(
    $"Original abilities: {string.Join(", ", original.Abilities)}");


Console.WriteLine("\n=== BUILDER ===\n");

Console.WriteLine(RegistrationCallSites.CreateLiveStudentUgly());
Console.WriteLine(RegistrationCallSites.CreateVideosOnlyUgly());

Console.WriteLine("\n=== BUILDER VALIDATION ===\n");

try
{
    new CourseRegistrationBuilder()
        .SetStudentEmail("test@mail.com")
        .SetCourseCode("SEF-101")
        .SetAccessMode("LiveGroup")
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected correctly: {ex.Message}");
}

try
{
    new CourseRegistrationBuilder()
        .SetStudentEmail("test@mail.com")
        .SetCourseCode("SEF-101")
        .SetAccessMode("VideosOnly")
        .SetGroupCode("G1")
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected correctly: {ex.Message}");
}