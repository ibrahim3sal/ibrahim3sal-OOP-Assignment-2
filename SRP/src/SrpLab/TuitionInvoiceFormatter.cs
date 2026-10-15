
namespace SrpLab;

public sealed class TuitionInvoiceFormatter
{
    public string Format(
        string courseCode,
        decimal tuition)
    {
        var vat = Math.Round(tuition * 0.14m, 2);

        return $"{courseCode},TUITION,{tuition:0.00},VAT,{vat:0.00},TOTAL,{(tuition + vat):0.00}";
    }

    public string FormatWaitlist(string courseCode)
    {
        return $"{courseCode},WAITLIST,0.00";
    }
}
