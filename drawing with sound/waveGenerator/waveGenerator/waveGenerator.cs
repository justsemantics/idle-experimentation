using System;
using System.IO;

public class WaveGenerator
{
    // Header, Format, Data chunks
    WaveHeader header;
    WaveFormatChunk format;
    WaveDataChunk data;

    WaveShape shape;

    int amplitude = 32760;  // Max amplitude for 16-bit audio
    double freq = 220.0f;   // Concert A: 440Hz

    public WaveGenerator()
    {
        // Init chunks
        header = new WaveHeader();
        format = new WaveFormatChunk();
        data = new WaveDataChunk();
    }

    public void SetWaveShape(Vector[] vertices)
    {
        shape = new WaveShape(vertices, format.dwSamplesPerSec, freq);
        shape.init();
    }

    public void Generate()
    {
        // Number of samples = sample rate * channels * bytes per sample
        uint numSamples = format.dwSamplesPerSec * format.wChannels * 10;

        // Initialize the 16-bit array
        data.floatArray = new short[numSamples];

        //double t = (Math.PI * 2 * freq) / (format.dwSamplesPerSec * format.wChannels);
        int lineIndex = 0;
        WaveLine currentLine = shape.lines[0];
        uint nextLineSample = 0;
        uint thisCycleStart = 0;
        for (uint i = 0; i < numSamples - 1; i+=2)
        {
            float progress = (float)(i - thisCycleStart - currentLine.startSample) / (float)currentLine.sampleLength;
            Vector position = currentLine.startPoint + (currentLine.endPoint - currentLine.startPoint) * progress;
            //write position data to left and right channels
            data.floatArray[i] = Convert.ToInt16(position.x * 50);
            data.floatArray[i + 1] = Convert.ToInt16(position.y * 50);

            if (i >= nextLineSample)
            {
                currentLine = shape.lines[lineIndex];
                if(lineIndex == 0)
                {
                    thisCycleStart = i;
                }
                lineIndex++;
                if (lineIndex == shape.lines.Length)
                {
                    lineIndex = 0;

                }

                nextLineSample = i + currentLine.sampleLength;
            }

        }

        // Calculate data chunk size in bytes
        data.dwChunkSize = (uint)(data.floatArray.Length * (format.wBitsPerSample / 8));
    }

    public void Save(string filePath)
    {
        // Create a file (it always overwrites)
        FileStream fileStream = new FileStream(filePath, FileMode.Create);   
    
        // Use BinaryWriter to write the bytes to the file
        BinaryWriter writer = new BinaryWriter(fileStream);
    
        // Write the header
        writer.Write(header.sGroupID.ToCharArray());
        writer.Write(header.dwFileLength);
        writer.Write(header.sRiffType.ToCharArray());
    
        // Write the format chunk
        writer.Write(format.sChunkID.ToCharArray());
        writer.Write(format.dwChunkSize);
        writer.Write(format.wFormatTag);
        writer.Write(format.wChannels);
        writer.Write(format.dwSamplesPerSec);
        writer.Write(format.dwAvgBytesPerSec);
        writer.Write(format.wBlockAlign);
        writer.Write(format.wBitsPerSample);
    
        // Write the data chunk
        writer.Write(data.sChunkID.ToCharArray());
        writer.Write(data.dwChunkSize);
        foreach (float dataPoint in data.floatArray)
        {
            writer.Write(dataPoint);
        }
    
        writer.Seek(4, SeekOrigin.Begin);
        uint filesize = (uint)writer.BaseStream.Length;
        writer.Write(filesize - 8);
       
        // Clean up
        writer.Close();
        fileStream.Close();            
    }
}

public class Vector{

    public float x;
    public float y;

    public Vector(float _x = 0, float _y = 0)
    {
        x = _x;
        y = _y;
    }

    public float magnitude()
    {
        return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
    }

    public Vector normal()
    {
        return (new Vector(x, y) / magnitude());
    }


    #region operators
    public static Vector operator +(Vector l, Vector r)
    {
        return new Vector(l.x + r.x, l.y + r.y);
    }

    public static Vector operator +(Vector l, float r)
    {
        return new Vector(l.x + r, l.y + r);
    }

    public static Vector operator -(Vector l, Vector r)
    {
        return new Vector(l.x - r.x, l.y - r.y);
    }

    public static Vector operator -(Vector l, float r)
    {
        return new Vector(l.x - r, l.y - r);
    }

    public static Vector operator *(Vector l, float r)
    {
        return new Vector(l.x * r, l.y * r);
    }

    public static Vector operator /(Vector l, float r)
    {
        return new Vector(l.x / r, l.y / r);
    }
    #endregion
}

public class WaveShape
{
    public Vector[] vertices;

    public WaveLine[] lines;

    uint sampleRate;

    double freq;

    public uint samplesPerCycle;

    public WaveShape(Vector[] _vertices, uint _sampleRate, double _freq)
    {
        vertices = _vertices;
        sampleRate = _sampleRate;
        freq = _freq;

        samplesPerCycle = (uint)Math.Floor(sampleRate / freq);
    }

    public void init()
    {
        float perimeter = 0;

        lines = new WaveLine[vertices.Length];

        Vector lastVertex;
        Vector nextVertex;

        for(int i = 0; i < vertices.Length; i++)
        {
            nextVertex = vertices[(i+1)%(vertices.Length)];
            lastVertex = vertices[i];

            Console.WriteLine("measuring ({0}, {1}) to ({2}, {3})", lastVertex.x, lastVertex.y, nextVertex.x, nextVertex.y);

            WaveLine segment = new WaveLine(lastVertex, nextVertex);
            lines[i] = segment;

            perimeter += segment.realLength;
        }
        uint currentSample = 0;

        Console.WriteLine(samplesPerCycle);
        foreach(WaveLine segment in lines)
        {
            segment.relativeLength = segment.realLength / perimeter;
            Console.WriteLine("Relative Length: " +segment.relativeLength);
            segment.sampleLength = (uint)Math.Floor(segment.relativeLength * samplesPerCycle);
            segment.startSample = currentSample;
            Console.WriteLine(segment.sampleLength + " starting from " + segment.startSample);
            segment.setUnitMovement();
            Console.WriteLine("line {0} unit movement is ({1}, {2})", 0, segment.unitMovement.x, segment.unitMovement.y);
            currentSample += segment.sampleLength;
        }
    }
}

public class WaveLine
{
    public Vector startPoint;
    public Vector endPoint;

    public Vector unitMovement;

    public uint startSample;
    public float realLength;
    public float relativeLength;
    public uint sampleLength;

    public WaveLine(Vector v1, Vector v2)
    {
        startPoint = v1;
        endPoint = v2;

        realLength = (endPoint - startPoint).magnitude();
    }

    public void setUnitMovement()
    {
        unitMovement = endPoint - startPoint;
        unitMovement = unitMovement.normal() / sampleLength;
    }
}