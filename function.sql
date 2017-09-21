CREATE OR REPLACE FUNCTION f_media_peso RETURN float IS
media FLOAT;
BEGIN
  SELECT Avg(peso) INTO media FROM Producao;
  RETURN round(media, 2);
END;

SELECT mediaPeso FROM producao;
