#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
GIT_VERSION=$($DIR/version.sh)

cd $DIR/../../../apps/shezzy/environment

ENV_FILE=".$1.env"

sed -i -e "s/VERSION=[0-9.]\+/VERSION=$GIT_VERSION/g" $ENV_FILE
sed -i -e "s/ENVIRONMENT=[abcdefghijklmnopqrstuvwxyz]\+/ENVIRONMENT=$1/g" $ENV_FILE

cd $DIR/../../../apps/shezzy

docker compose --env-file ./environment/.$1.env up