CREATE TABLE "book"(
    "id" VARCHAR(255) NOT NULL,
    "name" VARCHAR(255) NOT NULL,
    "genre_id" BIGINT NOT NULL,
    "description" VARCHAR(255) NOT NULL,
    "date_release" DATE NOT NULL,
    "status_id" BIGINT NOT NULL,
    "user_id" BIGINT NOT NULL
);
ALTER TABLE
    "book" ADD CONSTRAINT "book_id_primary" PRIMARY KEY("id");
CREATE TABLE "user"(
    "id" BIGINT NOT NULL IDENTITY(1,1),
    "login" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
    "date_reristr" DATE NOT NULL,
    "fio" VARCHAR(255) NOT NULL,
    "phone" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "user" ADD CONSTRAINT "user_id_primary" PRIMARY KEY("id");
CREATE TABLE "genre"(
    "id" BIGINT NOT NULL IDENTITY(1,1),
    "name" VARCHAR(255) NOT NULL,
    "description" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "genre" ADD CONSTRAINT "genre_id_primary" PRIMARY KEY("id");
CREATE TABLE "status"(
    "id" BIGINT NOT NULL IDENTITY(1,1),
    "content" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "status" ADD CONSTRAINT "status_id_primary" PRIMARY KEY("id");
ALTER TABLE
    "book" ADD CONSTRAINT "book_status_id_foreign" FOREIGN KEY("status_id") REFERENCES "status"("id");
ALTER TABLE
    "book" ADD CONSTRAINT "book_user_id_foreign" FOREIGN KEY("user_id") REFERENCES "user"("id");
ALTER TABLE
    "book" ADD CONSTRAINT "book_genre_id_foreign" FOREIGN KEY("genre_id") REFERENCES "genre"("id");