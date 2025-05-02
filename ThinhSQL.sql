SELECT * FROM public."Users"
ORDER BY "Username" ASC 

CREATE TABLE AIContent (
    Id SERIAL NOT NULL PRIMARY KEY,
    Content TEXT NOT NULL,
    UserID VARCHAR(255)
);

SELECT * FROM public.aicontent
ORDER BY "id" ASC 
