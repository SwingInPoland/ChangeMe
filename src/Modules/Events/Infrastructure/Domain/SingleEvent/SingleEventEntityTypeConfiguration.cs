using System.Text.Json;
using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventDate;
using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventStatus;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventHost;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventImage;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeMe.Modules.Events.Infrastructure.Domain.SingleEvent;

internal class SingleEventEntityTypeConfiguration : IEntityTypeConfiguration<Events.Domain.SingleEvent.SingleEvent>
{
    public void Configure(EntityTypeBuilder<Events.Domain.SingleEvent.SingleEvent> builder)
    {
        builder.ToTable("SingleEvents", "events");

        builder.HasKey(x => x.Id);

        builder.OwnsOne<EventNames>("_names", b =>
        {
            //TODO: Implement this
        });

        builder.OwnsOne<EventNames>("_descriptions", b =>
        {
            //TODO: Implement this
        });

        builder.OwnsOne<SingleEventDate>("_date", b =>
        {
            b.Property(p => p.StartDate).HasColumnName("StartDate");
            b.Property(p => p.EndDate).HasColumnName("EndDate");
        });

        builder.OwnsOne<EventHost>("_host", b =>
        {
            b.Property(p => p.Name).HasColumnName("HostName");
            b.Property(p => p.Url).HasColumnName("HostUrl");
        });

        builder.OwnsOne<EventImage>("_image", b => { b.Property(p => p.Value).HasColumnName("ImageUrl"); });
        builder.OwnsOne<EventUrl>("_url", b => { b.Property(p => p.Url).HasColumnName("EventUrl"); });

        builder.OwnsOne<EventLocation>("_location", b =>
        {
            b.OwnsOne(l => l.Coordinates, b =>
            {
                b.Property(p => p.Latitude).HasColumnName("LocationLatitude");
                b.Property(p => p.Longitude).HasColumnName("LocationLongitude");
            });

            b.OwnsOne(l => l.City, b => { b.Property(p => p.Value).HasColumnName("LocationCity"); });
            b.OwnsOne(l => l.Province, b => { b.Property(p => p.Value).HasColumnName("LocationProvince"); });
            b.OwnsOne(l => l.PostalCode, b => { b.Property(p => p.Value).HasColumnName("LocationPostalCode"); });
            b.OwnsOne(l => l.Name, b => { b.Property(p => p.Value).HasColumnName("LocationName"); });

            b.OwnsOne(l => l.Street, b =>
            {
                b.Property(p => p.StreetName).HasColumnName("LocationStreetName");
                b.Property(p => p.StreetNumber).HasColumnName("LocationStreetNumber");
                b.Property(p => p.AdditionalInfo).HasColumnName("LocationAdditionalInfo");
            });
        });

        builder.Property<bool>("_isForFree").HasColumnName("IsForFree");
        builder.OwnsOne<SingleEventStatus>("_status", b => { b.Property(p => p.Value).HasColumnName("Status"); });

        builder.Property<HashSet<string>>("_editors").HasColumnName("Editors")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<HashSet<string>>(v, (JsonSerializerOptions)null),
                new ValueComparer<HashSet<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c));


        builder.Property<string>("_creatorId").HasColumnName("CreatorId");
    }
}