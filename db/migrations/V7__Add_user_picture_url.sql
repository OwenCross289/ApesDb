ALTER TABLE "public"."Users"
    ADD COLUMN IF NOT EXISTS "PictureUrl" character varying(2048);
