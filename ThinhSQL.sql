CREATE TABLE Users (
    Id TEXT PRIMARY KEY,
    Username TEXT,
    Fullname TEXT,
    PasswordHash TEXT,
    Email TEXT
);
SELECT * FROM public."Users"
ORDER BY username ASC

CREATE TABLE AIContent (
    Id SERIAL NOT NULL PRIMARY KEY,
    Content TEXT NOT NULL,
    UserID VARCHAR(255)
);

SELECT * FROM public.aicontent
ORDER BY "id" ASC 
