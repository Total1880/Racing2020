using System;

namespace Racing.Messages
{
    class UpdateJerseyMessage
    {
        public Guid YellowId;
        public int DivisionId;

        public UpdateJerseyMessage(Guid yellowId, int divisionId)
        {
            YellowId = yellowId;
            DivisionId = divisionId;
        }
    }
}
