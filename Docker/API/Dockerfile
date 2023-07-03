FROM mono:6
ADD ../../SelXPressApi/SelXPressApi/ /home/API/SelXPressApi/
RUN mcs /home/API/SelXPressApi/program.cs
CMD [ "mono", "/home/API/SelXPressApi/program.exe" ]