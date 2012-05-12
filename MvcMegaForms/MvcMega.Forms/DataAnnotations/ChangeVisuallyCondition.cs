namespace MvcMega.Forms.DataAnnotations
{
    public class ChangeVisuallyCondition
    {
        public ChangeVisuallyCondition(ChangeVisuallyAttribute.ChangeTo to, string whenOtherPropertyName, ChangeVisuallyAttribute.DisplayChangeIf ifOperator, object value, bool conditionPassesIfNull)
        {
            To = to;
            WhenOtherPropertyName = whenOtherPropertyName;
            If = ifOperator;
            Value = value;
            ConditionPassesIfNull = conditionPassesIfNull;
        }

        public ChangeVisuallyAttribute.ChangeTo To { get; set; }

        public string WhenOtherPropertyName { get; set; }

        public ChangeVisuallyAttribute.DisplayChangeIf If { get; set; }

        public object Value { get; set; }

        public bool ConditionPassesIfNull { get; set; }
    }
}