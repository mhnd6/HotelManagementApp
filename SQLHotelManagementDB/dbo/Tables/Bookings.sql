﻿CREATE TABLE [dbo].[Bookings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RoomId] INT NOT NULL, 
    [StartDate] DATETIME2 NOT NULL, 
    [EndDate] DATETIME2 NOT NULL, 
    [GuestId] INT NOT NULL, 
    [TotalCost] MONEY NOT NULL, 
    [CheckedIn] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Bookings_Rooms] FOREIGN KEY (RoomId) REFERENCES Rooms(Id), 
    CONSTRAINT [FK_Bookings_Guests] FOREIGN KEY (GuestId) REFERENCES Guests(Id) 
)
