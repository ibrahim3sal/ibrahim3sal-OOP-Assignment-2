
namespace SrpLab;

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    private readonly PatientAcuityCalculator _acuityCalculator = new();
    private readonly PagerAlertService _pagerAlertService = new();
    private readonly HandoffNoteFormatter _handoffNoteFormatter = new();
    private readonly WardCensusExporter _censusExporter = new();

    public void AssignBed(
        int bed,
        string patientId,
        int heartRate,
        int spo2)
    {
        if (bed <= 0)
            throw new ArgumentOutOfRangeException(nameof(bed));

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();

        var acuity = _acuityCalculator.ScoreAcuity(heartRate, spo2);
        _vitalsScore[bed] = acuity;

        _pagerAlertService.CheckAndRecordAlert(bed, acuity);
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";

        return _handoffNoteFormatter.Format(
            bed,
            patient,
            _vitalsScore[bed]);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerAlertService.DrainPagerLog();
    }

    public string ExportCensusCsv()
    {
        return _censusExporter.ExportCsv(
            _bedPatient,
            _vitalsScore);
    }
}
