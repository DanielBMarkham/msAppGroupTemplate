#!/bin/bash

APPNAME=${1:-foo}


mkdir $APPNAME
mkdir $APPNAME/applib
mkdir $APPNAME/app
mkdir $APPNAME/appTest

cp  ./apptemplate/applib/* $APPNAME/applib
cp  ./apptemplate/app/* $APPNAME/app
cp  ./apptemplate/appTest/* $APPNAME/appTest




