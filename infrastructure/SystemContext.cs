using Microsoft.EntityFrameworkCore;

namespace AlarmSystem.Infrastructure {
    public class SystemContext : DbContext {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) {}

        
    }
}