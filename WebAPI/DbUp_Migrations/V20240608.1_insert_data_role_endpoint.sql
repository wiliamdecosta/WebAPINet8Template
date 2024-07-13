INSERT INTO role_endpoint(role_id, endpoint_id, created_date, updated_date)
SELECT 1, endpoint_id, now(), now() FROM endpoints;