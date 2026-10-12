
namespace SrpLab;

public sealed class GradeReportFormatter
{
    private readonly GradePolicy _gradePolicy;

    public GradeReportFormatter(GradePolicy gradePolicy)
    {
        _gradePolicy = gradePolicy;
    }

    public string TranscriptPlain(
        string studentId,
        string fullName,
        decimal average)
    {
        var letter = _gradePolicy.Letter(average);
        var honor = _gradePolicy.MeetsHonorRoll(average);

        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {honor}\n";
    }

    public string ExportCsv(GradeBook gradeBook)
    {
        var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

        foreach (var id in gradeBook.StudentIds.OrderBy(id => id))
        {
            var average = gradeBook.Average(id);
            var letter = _gradePolicy.Letter(average);
            var honor = _gradePolicy.MeetsHonorRoll(average);

            rows.Add(
                $"{id},{average},{letter},{(honor ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}
