using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Books.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Bio = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BookId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    BookId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("1480198c-b786-447b-8c10-d068f0988192"), "George Raymond Richard Martin, commonly known as George R.R. Martin, is an American novelist and short story writer. He is best known for his series of epic fantasy novels, 'A Song of Ice and Fire', which has been adapted into the television series 'Game of Thrones'. Martin's writing is known for its complex characters, intricate plotlines, and realistic portrayal of political and social dynamics. His work has garnered critical acclaim and has a vast and dedicated fanbase.", "George R.R.", "Martin" },
                    { new Guid("44390cc5-3f32-4df6-aab3-39480f87788a"), "A British author best known for the Harry Potter series.", "J.K.", "Rowling" },
                    { new Guid("7482e7b2-1c57-4d29-825d-5d5e58f5301c"), "An English writer and the son of J.R.R. Tolkien.", "Christopher", "Tolkien" },
                    { new Guid("78b58114-1fc0-4f9c-b5f4-2b16d352f543"), "An English novelist known for her witty and insightful writing.", "Jane", "Austen" },
                    { new Guid("87229a28-033d-4f85-af7c-0e5aaaa1e49c"), "An English author known for his political and social commentary.", "George", "Orwell" },
                    { new Guid("90118e72-df25-4e1e-ac27-35e15533daa0"), "An American writer associated with the Jazz Age.", "F. Scott", "Fitzgerald" },
                    { new Guid("d6a62e9d-af3a-4c7d-80fa-2612215f9af4"), "A Russian novelist and philosopher.", "Fyodor", "Dostoevsky" },
                    { new Guid("e026f2c2-bd9d-4f3d-95a7-3411eec89a4d"), "An English writer, poet, and philologist.", "J.R.R.", "Tolkien" },
                    { new Guid("e9715948-560f-4e50-8965-7cd61c480768"), "An American novelist best known for her acclaimed work.", "Harper", "Lee" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Title" },
                values: new object[,]
                {
                    { new Guid("06783e47-d5d5-43c6-b75e-d3b4ae2b7cae"), "wshakespeare", new DateTime(2011, 3, 13, 10, 16, 0, 0, DateTimeKind.Unspecified), "A novel set in the 1950s and considered a sequel to To Kill a Mockingbird.", null, null, "Go Set a Watchman" },
                    { new Guid("0881e81c-fbfc-47ff-bc0d-5670aca11601"), "aeinstein", new DateTime(2002, 4, 16, 2, 28, 0, 0, DateTimeKind.Unspecified), "The power struggles intensify as war engulfs the Seven Kingdoms.", null, null, "A Storm of Swords" },
                    { new Guid("17d9e08f-1a3d-4148-9521-69cbc3f00018"), "mcurie", new DateTime(2008, 1, 9, 11, 38, 0, 0, DateTimeKind.Unspecified), "A dystopian novel that explores the dangers of totalitarianism.", null, null, "1984" },
                    { new Guid("1d2eb21a-7f00-440e-8d91-af1f373124c6"), "wshakespeare", new DateTime(2000, 8, 10, 1, 52, 0, 0, DateTimeKind.Unspecified), "A psychological novel exploring the moral and psychological struggles of its main character.", null, null, "Crime and Punishment" },
                    { new Guid("5485c003-57cc-4628-a1b8-34ccbe35c587"), "jcaesar", new DateTime(2008, 3, 10, 11, 27, 0, 0, DateTimeKind.Unspecified), "The first book in the series, introducing the world of Westeros and its political intrigues.", null, null, "A Game of Thrones" },
                    { new Guid("5a176e35-fd4e-4ae0-8f6b-709eeaaa2827"), "mcurie", new DateTime(2014, 7, 12, 1, 41, 0, 0, DateTimeKind.Unspecified), "A collection of mythopoeic works exploring the mythology of Middle-earth.", null, null, "The Silmarillion" },
                    { new Guid("68fcf3ea-711a-4a91-943c-8df2bab7746d"), "ntesla", new DateTime(2014, 12, 3, 20, 42, 0, 0, DateTimeKind.Unspecified), "A companion book to the Harry Potter series, featuring magical creatures.", null, null, "Fantastic Beasts and Where to Find Them" },
                    { new Guid("7c838a53-cac5-4ebe-94cb-c9b46c537c4e"), "ldavinci", new DateTime(2006, 3, 24, 18, 33, 0, 0, DateTimeKind.Unspecified), "Continues the story as multiple claimants vie for the Iron Throne.", null, null, "A Clash of Kings" },
                    { new Guid("7ca4a6d0-1f6d-4bd5-9d60-16e48a39563b"), "ldavinci", new DateTime(2014, 6, 21, 8, 15, 0, 0, DateTimeKind.Unspecified), "Explores the aftermath of the previous conflicts and the shifting power dynamics.", null, null, "A Feast for Crows" },
                    { new Guid("7e10a5f0-32f3-489c-ac99-468b400a3757"), "aeinstein", new DateTime(2012, 6, 27, 23, 44, 0, 0, DateTimeKind.Unspecified), "A story of wealth, love, and the American Dream during the Roaring Twenties.", null, null, "The Great Gatsby" },
                    { new Guid("8201ad58-0097-4852-adbb-38046938595b"), "ldavinci", new DateTime(2012, 5, 25, 18, 44, 0, 0, DateTimeKind.Unspecified), "A gripping novel set in the Deep South during the era of racial inequality.", null, null, "To Kill a Mockingbird" },
                    { new Guid("8f33b67c-5080-4132-9da6-43c1cf348cc0"), "wgenghis", new DateTime(2019, 11, 19, 4, 11, 0, 0, DateTimeKind.Unspecified), "An epic fantasy trilogy set in the fictional world of Middle-earth.", null, null, "The Lord of the Rings" },
                    { new Guid("9407e459-042d-48f0-a9ec-3abf49b81649"), "aalexander", new DateTime(2021, 3, 24, 8, 36, 0, 0, DateTimeKind.Unspecified), "Continues the storylines of various characters as conflicts escalate.", null, null, "A Dance with Dragons" },
                    { new Guid("b0110f8d-a9db-44b3-9373-91e7b3006e61"), "ntesla", new DateTime(2000, 5, 23, 16, 24, 0, 0, DateTimeKind.Unspecified), "A classic romantic novel set in 19th-century England.", null, null, "Pride and Prejudice" },
                    { new Guid("b0e60fb8-a198-4b27-a935-54aa02d07a2c"), "alincoln", new DateTime(2001, 6, 18, 22, 1, 0, 0, DateTimeKind.Unspecified), "The first book in the popular Harry Potter series.", null, null, "Harry Potter and the Sorcerer's Stone" },
                    { new Guid("ebde1cdd-35f3-4ea6-8984-0d7d1302e257"), "aeinstein", new DateTime(2002, 8, 7, 16, 27, 0, 0, DateTimeKind.Unspecified), "A fantasy novel that serves as a prequel to The Lord of the Rings.", null, null, "The Hobbit" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("20a8e0ea-e5be-4933-bf24-634395321e4d"), "caesar.julius@example.com", "Julius", "Caesar", "AQAAAAIAAYagAAAAEJlXx19Siv0fTfKFNz8bWzwKrHr3M40Wlr/17A2T0XUinu+wPKdhFJyWjrQD0uN1kQ==", "jcaesar" },
                    { new Guid("42b8c308-1aaa-4038-8201-fdd835527472"), "genghis.khan@example.com", "Genghis", "Khan", "AQAAAAIAAYagAAAAEDOqf/bmA7tTtRmAuN+50C/6ZpmW/UEf7bDSPUckoqW7V1NjTFK6CjtN9uoay2Nmgg==", "wgenghis" },
                    { new Guid("5cf7827d-1d79-4ab9-b0c8-564913450e77"), "tesla.nikola@example.com", "Nikola", "Tesla", "AQAAAAIAAYagAAAAEEK2HlKj3mO8OqqV4Y6P8jrjn/g5iH4bfp6RUXyMhXJB2xp+yGvtuPJD7qf/S/kSsw==", "ntesla" },
                    { new Guid("67268812-e309-4ac9-bd1d-c9229eb69817"), "davinci.leonardo@example.com", "Leonardo", "da Vinci", "AQAAAAIAAYagAAAAEGtcQaTtDoAGVFxOoVWay8M46f/N3FZhE8/sPCZH8U8Y/uYvrH2hHuoF9fDvRzRQEg==", "ldavinci" },
                    { new Guid("8b6eda7b-7ffb-4031-ad4d-90590c91d3ce"), "lincoln.abraham@example.com", "Abraham", "Lincoln", "AQAAAAIAAYagAAAAENjKNRElvP5XUWfmhbpzCLpvWaAd1fCgCJOJQ/2/I8VWqluPBTpC76YtE3Cu7Usw9w==", "alincoln" },
                    { new Guid("9a86f182-fef7-46f8-b71a-ac6ea2068633"), "cleopatra@example.com", "Cleopatra", "", "AQAAAAIAAYagAAAAECNNQKRjoc1FwqJz19kkFCplW0KLpmP/TQjuEwz5WBs0i/MzxDjgk7ZYS5xGtGU6bA==", "ccleopatra" },
                    { new Guid("d1c6720f-38b1-46c9-8907-1b7b7c41eadb"), "curie.marie@example.com", "Marie", "Curie", "AQAAAAIAAYagAAAAEB4uxGp6GUucmKAYOxNlDeufRg3aTuLary8z9SJjiOcEn8h5ks45oRpf9GI7OrFwRg==", "mcurie" },
                    { new Guid("dc85a3d5-cb0e-4bc1-b3cd-8e4c4dde3913"), "shakespeare.william@example.com", "William", "Shakespeare", "AQAAAAIAAYagAAAAENLvWBQPKPOmuxr1cDeOxO+giWA/P+kXKJxjKnrsoeQ5dY0fKigZhI93Z1yhA9bCDQ==", "wshakespeare" },
                    { new Guid("e06f0e3d-6a26-4495-bdbf-763e8dc49400"), "einstein.albert@example.com", "Albert", "Einstein", "AQAAAAIAAYagAAAAENnIWf5ct4xPjNWUD77Ep21+miK2z4Yh4OHFAS9h+FDld7gVM6OJVbvJnaD/OBVVBA==", "aeinstein" },
                    { new Guid("f8cd6f8d-a49c-4d96-9723-0095c0daeb37"), "alexander@example.com", "Alexander", "the Great", "AQAAAAIAAYagAAAAEBxG4JlJGKOUB/zUGkam6GI4JrbRoMeOjW/wqnUoK2ObG77tGhGzlHjiQtCQ4qAC2Q==", "aalexander" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId", "CreatedAt" },
                values: new object[,]
                {
                    { new Guid("1480198c-b786-447b-8c10-d068f0988192"), new Guid("0881e81c-fbfc-47ff-bc0d-5670aca11601"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1297) },
                    { new Guid("1480198c-b786-447b-8c10-d068f0988192"), new Guid("5485c003-57cc-4628-a1b8-34ccbe35c587"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1291) },
                    { new Guid("1480198c-b786-447b-8c10-d068f0988192"), new Guid("7c838a53-cac5-4ebe-94cb-c9b46c537c4e"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1296) },
                    { new Guid("1480198c-b786-447b-8c10-d068f0988192"), new Guid("7ca4a6d0-1f6d-4bd5-9d60-16e48a39563b"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1299) },
                    { new Guid("1480198c-b786-447b-8c10-d068f0988192"), new Guid("9407e459-042d-48f0-a9ec-3abf49b81649"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1300) },
                    { new Guid("44390cc5-3f32-4df6-aab3-39480f87788a"), new Guid("68fcf3ea-711a-4a91-943c-8df2bab7746d"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1307) },
                    { new Guid("44390cc5-3f32-4df6-aab3-39480f87788a"), new Guid("b0e60fb8-a198-4b27-a935-54aa02d07a2c"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1306) },
                    { new Guid("7482e7b2-1c57-4d29-825d-5d5e58f5301c"), new Guid("8f33b67c-5080-4132-9da6-43c1cf348cc0"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1303) },
                    { new Guid("78b58114-1fc0-4f9c-b5f4-2b16d352f543"), new Guid("b0110f8d-a9db-44b3-9373-91e7b3006e61"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1312) },
                    { new Guid("87229a28-033d-4f85-af7c-0e5aaaa1e49c"), new Guid("17d9e08f-1a3d-4148-9521-69cbc3f00018"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1311) },
                    { new Guid("90118e72-df25-4e1e-ac27-35e15533daa0"), new Guid("7e10a5f0-32f3-489c-ac99-468b400a3757"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1313) },
                    { new Guid("d6a62e9d-af3a-4c7d-80fa-2612215f9af4"), new Guid("1d2eb21a-7f00-440e-8d91-af1f373124c6"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1308) },
                    { new Guid("e026f2c2-bd9d-4f3d-95a7-3411eec89a4d"), new Guid("5a176e35-fd4e-4ae0-8f6b-709eeaaa2827"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1305) },
                    { new Guid("e026f2c2-bd9d-4f3d-95a7-3411eec89a4d"), new Guid("8f33b67c-5080-4132-9da6-43c1cf348cc0"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1301) },
                    { new Guid("e026f2c2-bd9d-4f3d-95a7-3411eec89a4d"), new Guid("ebde1cdd-35f3-4ea6-8984-0d7d1302e257"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1304) },
                    { new Guid("e9715948-560f-4e50-8965-7cd61c480768"), new Guid("06783e47-d5d5-43c6-b75e-d3b4ae2b7cae"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1310) },
                    { new Guid("e9715948-560f-4e50-8965-7cd61c480768"), new Guid("8201ad58-0097-4852-adbb-38046938595b"), new DateTime(2023, 5, 16, 18, 26, 9, 457, DateTimeKind.Utc).AddTicks(1309) }
                });

            migrationBuilder.InsertData(
                table: "BookReviews",
                columns: new[] { "Id", "BookId", "Comment", "Username" },
                values: new object[,]
                {
                    { new Guid("20a2f45a-b6f0-4c41-b7b3-458c7632ac99"), new Guid("7e10a5f0-32f3-489c-ac99-468b400a3757"), "An iconic representation of the Jazz Age.", "ccleopatra" },
                    { new Guid("3baa7952-3374-4f05-9618-33e999acb928"), new Guid("7c838a53-cac5-4ebe-94cb-c9b46c537c4e"), "Captivating and immersive, loved every page.", "mcurie" },
                    { new Guid("414eecf8-a7d9-4e6c-847c-8492c4b6353f"), new Guid("8201ad58-0097-4852-adbb-38046938595b"), "A classic that stands the test of time.", "aeinstein" },
                    { new Guid("467f736e-0fb5-42a5-ab51-bcf6d6629d32"), new Guid("1d2eb21a-7f00-440e-8d91-af1f373124c6"), "A gripping and powerful novel.", "alincoln" },
                    { new Guid("529f960d-c0eb-49c6-8b4f-2b18c599c39b"), new Guid("5a176e35-fd4e-4ae0-8f6b-709eeaaa2827"), "Intriguing characters and rich storytelling.", "ccleopatra" },
                    { new Guid("5b4f0137-7810-4e7c-8a74-41eead28a1dc"), new Guid("8201ad58-0097-4852-adbb-38046938595b"), "A remarkable and poignant story.", "mcurie" },
                    { new Guid("77110fa4-f4a1-4e76-9774-02455fe9e2e7"), new Guid("8f33b67c-5080-4132-9da6-43c1cf348cc0"), "A masterpiece of fantasy literature.", "ldavinci" },
                    { new Guid("7d23337f-a760-4685-8ef4-42952fa64a7b"), new Guid("5485c003-57cc-4628-a1b8-34ccbe35c587"), "Great book, highly recommended!", "alincoln" },
                    { new Guid("7e249198-9d24-4024-93ca-f342c35e36d3"), new Guid("b0e60fb8-a198-4b27-a935-54aa02d07a2c"), "Thought-provoking and profound.", "wgenghis" },
                    { new Guid("84a87346-2e80-4594-82ba-2f09fe71bebe"), new Guid("17d9e08f-1a3d-4148-9521-69cbc3f00018"), "An unsettling and thought-provoking dystopian tale.", "ldavinci" },
                    { new Guid("8bd2faa0-5738-465f-be31-06e72a4e95e6"), new Guid("ebde1cdd-35f3-4ea6-8984-0d7d1302e257"), "Beautifully written, a timeless classic.", "wshakespeare" },
                    { new Guid("993a5f82-f2a3-4e80-930d-8baf047b36bc"), new Guid("5485c003-57cc-4628-a1b8-34ccbe35c587"), "I couldn't put it down, fantastic read!", "jcaesar" },
                    { new Guid("a8e14588-aeb2-4459-b792-474bcb014066"), new Guid("b0e60fb8-a198-4b27-a935-54aa02d07a2c"), "Magical and enchanting, a must-read for all ages.", "ntesla" },
                    { new Guid("aae94120-0087-48a1-8537-d61b7068e5b8"), new Guid("1d2eb21a-7f00-440e-8d91-af1f373124c6"), "This book changed my perspective on life.", "jcaesar" },
                    { new Guid("b677b2ca-322f-4543-85f7-1ea8e93b4d20"), new Guid("b0110f8d-a9db-44b3-9373-91e7b3006e61"), "A delightful and engaging romance.", "wshakespeare" },
                    { new Guid("d1f9368d-7961-4fd1-af22-c6c06f773d14"), new Guid("5a176e35-fd4e-4ae0-8f6b-709eeaaa2827"), "A fascinating journey through Middle-earth.", "aalexander" },
                    { new Guid("e7e6c93b-47e3-4b1b-8384-caaa8284c347"), new Guid("7c838a53-cac5-4ebe-94cb-c9b46c537c4e"), "An epic tale with intricate plot twists.", "aeinstein" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "BookReviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
