#!/bin/bash

# requires that rename be installed

APPNAME=${1:-foo}


mkdir $APPNAME
mkdir $APPNAME/${APPNAME}lib
mkdir $APPNAME/${APPNAME}
mkdir $APPNAME/${APPNAME}Test

cp  ./apptemplate/applib/* $APPNAME/${APPNAME}lib
cp  ./apptemplate/app/* $APPNAME/${APPNAME}
cp  ./apptemplate/appTest/* $APPNAME/${APPNAME}Test

cd $APPNAME/${APPNAME}lib
for i in *
do
    mv "$i" "${i/app/"$APPNAME"}"
done
sed -i "s/\\\app/$APPNAME/" *
sed -i "s/applib/${APPNAME}""lib/" *
sed -i "s/appTest/${APPNAME}""Test/" *
sed -i "s/groupCommon"$APPNAME"/groupCommon\\\app/" *

cd ../${APPNAME}
for i in *
do
    mv "$i" "${i/app/"$APPNAME"}"
done
sed -i "s/\\\app/$APPNAME/" *
sed -i "s/applib/${APPNAME}""lib/" *
sed -i "s/appTest/${APPNAME}""Test/" *
sed -i "s/groupCommon"$APPNAME"/groupCommon\\\app/" *

cd ../${APPNAME}Test
for i in *
do
    mv "$i" "${i/app/"$APPNAME"}"
done
sed -i "s/\\\app/$APPNAME/" *
sed -i "s/applib/${APPNAME}""lib/" *
sed -i "s/appTest/${APPNAME}""Test/" *
sed -i "s/groupCommon"$APPNAME"/groupCommon\\\app/" *
