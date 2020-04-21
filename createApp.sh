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
    mv "$i" "${i/app/"$APPNAME"}" || :
done 
sed -i "s/\\\app/$APPNAME/" * || :
sed -i "s/applib/${APPNAME}""lib/" * || :
sed -i "s/appTest/${APPNAME}""Test/" * || :
#echo "d\n\n" || :
sed -i "s/groupCommon"$APPNAME"/groupCommon\\\app/" *
sed -i "s/Include=\"\.\./Include=\"\.\.\\\/" *.fsproj
sed -i "s/app.fsproj/"${APPNAME}".fsproj/" *.fsproj
#echo "w\n\n"
sed -i "s/namespace app/namespace "${APPNAME}"/" *.fs
sed -i "s/namespace app/namespace "${APPNAME}"/" *.fsi
sed -i "s/open app/open "${APPNAME}"/" *.fs
sed -i "s/open app/open "${APPNAME}"/" *.fsi
#echo "done 1"

cd ../${APPNAME}
for i in *
do
    mv "$i" "${i/app/"$APPNAME"}" || :
done
sed -i "s/\\\app/$APPNAME/" *
sed -i "s/applib/${APPNAME}""lib/" *
sed -i "s/appTest/${APPNAME}""Test/" *
sed -i "s/groupCommon"$APPNAME"/groupCommon\\\app/" *
sed -i "s/Include=\"\.\./Include=\"\.\.\\\/" *.fsproj
sed -i "s/app.fsproj/"${APPNAME}".fsproj/" *.fsproj
sed -i "s/namespace app/namespace "${APPNAME}"/" *.fs
sed -i "s/namespace app/namespace "${APPNAME}"/" *.fsi
sed -i "s/open app/open "${APPNAME}"/" *.fs
sed -i "s/open app/open "${APPNAME}"/" *.fsi

#echo "done 2"



cd ../${APPNAME}Test
for i in *
do
    mv "$i" "${i/app/"$APPNAME"}" || :
done
sed -i "s/\\\app/$APPNAME/" *
sed -i "s/applib/${APPNAME}""lib/" *
sed -i "s/appTest/${APPNAME}""Test/" *
sed -i "s/groupCommon"$APPNAME"/groupCommon\\\app/" *
sed -i "s/Include=\"\.\./Include=\"\.\.\\\/" *.fsproj
sed -i "s/app.fsproj/"${APPNAME}".fsproj/" *.fsproj
sed -i "s/namespace app/namespace "${APPNAME}"/" *.fs
sed -i "s/namespace app/namespace "${APPNAME}"/" *.fsi
sed -i "s/open app/open "${APPNAME}"/" *.fs
sed -i "s/open app/open "${APPNAME}"/" *.fsi

#echo "done 3"

echo "\nSCRIPT DONE\n"

#sed -i "s/app.fsproj/"${pop}".fsproj/" *.fsproj