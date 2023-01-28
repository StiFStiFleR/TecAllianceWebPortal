﻿namespace TecAllianceWebPortal.Model
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
