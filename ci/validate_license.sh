#!/usr/bin/env bash

set -x

echo "Validating Licenses"

openssl aes-256-cbc -d -in .circleci/Unity_v2019.x.ulf-cipher -k ${CIPHER_KEY} >> .circleci/Unity_v2019.x.ulf

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
  -quit \
  -nographics \
  -logFile \
  -batchmode \
  -manualLicenseFile .circleci/Unity_v2019.x.ulf || exit 0
