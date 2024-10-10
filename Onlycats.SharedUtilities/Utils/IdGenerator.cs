namespace Onlycats.SharedUtilities.utils{
    public class IdGenerator {

        private static IdGenerator? instance = null;

        public static IdGenerator Instance {
            get {
                // instance ??= new IdGenerator();
                if (instance == null) {
                    instance = new IdGenerator();
                }
                return instance;
            }
        }

        private static int lastId = 0;

        public static int GenerateId() {
            lastId++;
            var now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)(now.ToUniversalTime() - unixEpoch).TotalSeconds+lastId;
        }
    }
}