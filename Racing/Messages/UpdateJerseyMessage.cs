namespace Racing.Messages
{
    class UpdateJerseyMessage
    {
        public int YellowId;
        public int DivisionId;

        public UpdateJerseyMessage(int yellowId, int divisionId)
        {
            YellowId = yellowId;
            DivisionId = divisionId;
        }
    }
}
