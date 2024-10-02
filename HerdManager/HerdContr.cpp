#include <iostream>
#include <stdlib.h>
#include <stdint.h>
#include <cmath>

using namespace std;

struct vectorPointer
{
    _Float32 di3Vec[3];
    _Float32 pos3Vec[3];
    uint32_t blobId;
    uint32_t memberId;
    vectorPointer* nextPos;//this is a pointer to the next data that is in the same map position
    vectorPointer* nextBlob;//this is a pointer to the next data that is part of the same blob
    //because of these two pointers, make sure blob and pos data are deleted at the same time, and don't reference
    //in between deletion
};
struct vector3F{
    public: 
        float x;
        float y;
        float z;
        int blobId = -1;
};
struct blob{
    _Float32 pos3Vec[3];
    _Float32 di3Vec[3];
    _Float32 blobMemberDist[3];//this float gives the memberdistribution, so the sort of eliptical shape of the blob
    uint32_t memberCount;
    vectorPointer* head;
};
void freeVector(vectorPointer* p){
    if(p)
    {
        freeVector(p->nextPos);
    }
    free(p);
    return;
}

//Now I know this is dirty and I should declare a seperate hpp file, but I think I am just defining this one class and its functions
class mapManager{
    public:
        mapManager(int x, int y, float stepE, int memberCount)
        {
            newMap = (vectorPointer***)malloc(x * sizeof(vectorPointer**));
            while(x > 0)
            {
                newMap[--x] = (vectorPointer**)malloc(y * sizeof(vectorPointer*));
            }
            mapX = x;
            mapY = y;
            step = stepE;
            newBlob = (blob*)malloc((memberCount / 2) * sizeof(blob));//unsafe due to integer rounding
            return;
        };
        ~mapManager()
        {//this is nightmerish, luckily this can just be done on a seperate thread
            for(int i = 0; i < mapX; i++)
            {
                for(int j = 0; j < mapY; j++)
                {
                    if(newMap[i][j]){
                        freeVector(newMap[i][j]);
                    }
                }
                free(newMap[i]);
            }
            free(preMap);
            for(int i = 0; i < mapX; i++)
            {
                for(int j = 0; j < mapY; j++)
                {
                    if(preMap[i][j]){
                        freeVector(preMap[i][j]);
                    }
                }
                free(preMap[i]);
            }
            free(preMap);
            
            free(newBlob);
            free(preBlob);
        }
        void startProcessing(int memberCount)
        {
            for(int i = 0; i < mapX; i++)
            {
                for(int j = 0; j < mapY; j++)
                {
                    if(preMap[i][j]){
                        freeVector(preMap[i][j]);
                    }
                }
                free(preMap[i]);
            }
            free(preMap);
            preMap = newMap;
            newMap = (vectorPointer***)malloc(mapX * sizeof(vectorPointer**));
            for(int i = 0; i < mapX; i++)
            {
                newMap[i] = (vectorPointer**)malloc(mapY * sizeof(vectorPointer*));
            }

            preBlob = newBlob;
            newBlob = (blob*)malloc(memberCount * sizeof(blob));
            return;
        }
        vector3F processMember(float x, float y, float z, int memberID)
        {
            vector3F retVec;
            int32_t xStep = (int32_t)(x / step);
            int32_t yStep = (int32_t)(y / step);

            //this is getting a bit complicated
            //this section is supposed to process the surrounding area of a member, to check they don't
            //"step on other members toes"
            //the float "t" is being used as a placeholder variable for the search radius
            //for now the search radius will be a square not a circle, i think it is more efficient
            //to use something like a circle, but for it to be more effecient the circle sizes need
            //to be hardcoded, or the positions in the array that the circles target need to be
            //hardcoded/instantiate-at-object-creation. This would take a lot of time, so I'm not going to do it
            int32_t t = 5;
            for(int32_t i = std::max<int32_t>(0, -t + xStep); i <= std::min<int32_t>(mapY,t + xStep); i++)
            {//searches square
                for(int32_t j = std::max<int32_t>(0, -t + xStep); j <= std::min<int32_t>(mapY,t + xStep); j++)
                {
                    _Float32 diVec[6];
                    vectorPointer* p = preMap[xStep + i][yStep + j];
                    while(p)
                    {//there are members at this position
                        float xd = p->pos3Vec[0] - x;
                        float yd = p->pos3Vec[1] - y;
                        float zd = p->pos3Vec[2] - z;
                        float distance = sqrtf32((xd * xd) + (yd * yd) + (zd * zd));

                        //in here we would adjust for the surrounding position, but I don't know how to do that yet

                        if(p->blobId != -1)
                        {

                        }
                        else
                        {

                        }

                        p = p->nextPos;
                    }
                }
            }


            //add to new blobs
            //first checking blobs, than check surrounding area to see if can add-to/create-new-blob
            uint32_t blobAssigned = 0;
            for(int i = 0; i < newBlobCount; i++)
            {
                _Float32 range = 10 + newBlob[i].memberCount;//simple range implementation right now
                //eventually need the range to be defined in the blob and have it be based of member count somehow
                _Float32 disFromBlobCenter =  x - newBlob[i].pos3Vec[0];
                disFromBlobCenter = disFromBlobCenter < 0 ? -disFromBlobCenter : disFromBlobCenter;
                if(disFromBlobCenter > range * newBlob[i].blobMemberDist[0])
                {
                    continue;
                    //distance is from blob is greater than allowed distance continue
                }
                disFromBlobCenter = y - newBlob[i].pos3Vec[1];
                disFromBlobCenter = disFromBlobCenter < 0 ? -disFromBlobCenter : disFromBlobCenter;
                if(disFromBlobCenter > range * newBlob[i].blobMemberDist[1])
                {
                    continue;
                }
                //check for z in the future, not doing that now
                // disFromBlobCenter = z - newBlob[i].pos3Vec[2];
                // disFromBlobCenter = disFromBlobCenter < 0 ? -disFromBlobCenter : disFromBlobCenter;
                // if(disFromBlobCenter > range * newBlob[i].blobMemberDist[2])
                // {
                //     continue;
                // }

                blobAssigned = 1;
                break;
            }

            if(!blobAssigned){
                int32_t t = 5;
                for(int32_t i = std::max<int32_t>(0, -t + xStep); i <= std::min<int32_t>(mapY,t + xStep); i++)
                {//searches square
                    for(int32_t j = std::max<int32_t>(0, -t + xStep); j <= std::min<int32_t>(mapY,t + xStep); j++)
                    {
                        _Float32 diVec[6];
                        vectorPointer* p = newMap[xStep + i][yStep + j];
                        while(p)
                        {//there are members at this position
                            float xd = p->pos3Vec[0] - x;
                            float yd = p->pos3Vec[1] - y;
                            float zd = p->pos3Vec[2] - z;
                            float distance = sqrtf32((xd * xd) + (yd * yd) + (zd * zd));

                            //in here we would adjust for the surrounding position, but I don't know how to do that yet

                            p = p->nextPos;
                        }
                    }
                }
            }
        }
    private:
        vectorPointer*** preMap;
        vectorPointer*** newMap;
        uint32_t mapX;
        uint32_t mapY;
        _Float32 step;
        blob* preBlob;
        blob* newBlob;
        uint32_t preBlobCount;
        uint32_t newBlobCount; 
};

extern "C"{ 
    
    mapManager* create(int x, int y, float step, int memberCount)
    {
        return new mapManager(x, y, step, memberCount);
    }

    void destroy(mapManager* b)
    {
        delete b;
    }

    vector3F delta()
    {
        vector3F c;
        c.xP = 11;
        return c;
    }

    vector3F processMember(mapManager* map, float x, float y, float z, int memberID)
    {
        return map->processMember(x, y, z, memberID);
    }
}