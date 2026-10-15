
namespace SrpLab;

public sealed class SupportMessageFormatter
{
    public string DraftPublicReply(
        string id,
        string priority,
        string agentName,
        DateTimeOffset slaDeadline)
    {
        var apology = priority == "P1"
            ? "We are treating this as a critical incident."
            : "Thanks for reaching out.";

        return $"Hi,\n{apology}\nTicket {id} is with {agentName}. Next update before {slaDeadline:u}.\n";
    }

    public string InternalEscalationBlurb(
        string id,
        string priority,
        DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {id} priority={priority} breachAt={slaDeadline:u} keywords-scanned=yes";
    }
}
