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


CREATE SEQUENCE aicontent_id_seq;
ALTER TABLE public."AIContent" 
ALTER COLUMN "Id" SET DEFAULT nextval('aicontent_id_seq');
ALTER SEQUENCE aicontent_id_seq OWNED BY "AIContent"."Id";
nhớ tắt identity của cột Id