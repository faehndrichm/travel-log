#!/bin/sh
# wait for MinIO to start
sleep 5

# configure mc alias and create bucket
mc alias set local http://minio:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
mc mb local/$BUCKET_NAME || true  # ignore if it already exists
