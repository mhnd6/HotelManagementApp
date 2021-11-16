CREATE PROCEDURE [dbo].[spBookings_CheckedIn]
	@Id int
AS
begin
	set nocount on;

	update dbo.Bookings
	SET CheckedIn = 1
	where Id = @Id;

end

