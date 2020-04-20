#!/bin/bash

APPNAME=${1:-foo}


mkdir $APPNAME

cp -r ./apptemplate/applib $APPNAME
cp -r ./apptemplate/app $APPNAME
cp -r ./apptemplate/appTest $APPNAME



