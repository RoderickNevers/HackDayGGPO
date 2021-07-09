using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

//using static VWConstants;

//public static class VWConstants
//{
//    public const int MAX_SHIPS = 4;
//    public const int MAX_PLAYERS = 64;

//    public const int INPUT_THRUST = (1 << 0);
//    public const int INPUT_BREAK = (1 << 1);
//    public const int INPUT_ROTATE_LEFT = (1 << 2);
//    public const int INPUT_ROTATE_RIGHT = (1 << 3);
//    public const int INPUT_FIRE = (1 << 4);
//    public const int INPUT_BOMB = (1 << 5);
//    public const int MAX_BULLETS = 30;

//    public const float PI = 3.1415926f;
//    public const int STARTING_HEALTH = 100;
//    public const float ROTATE_INCREMENT = 3f;
//    public const float SHIP_RADIUS = 15f;
//    public const float SHIP_THRUST = 0.06f;
//    public const float SHIP_MAX_THRUST = 4.0f;
//    public const float SHIP_BREAK_SPEED = 0.6f;
//    public const float BULLET_SPEED = 5f;
//    public const int BULLET_COOLDOWN = 8;
//    public const int BULLET_DAMAGE = 10;
//}

[Serializable]
public struct Player
{
    public Vector3 position;
    public Vector3 velocity;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        bw.Write(position.z);
        bw.Write(velocity.x);
        bw.Write(velocity.z);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.z = br.ReadSingle();
        velocity.x = br.ReadSingle();
        velocity.z = br.ReadSingle();
    }

    // @LOOK Not hashing bullets.
    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + position.GetHashCode();
        hashCode = hashCode * -1521134295 + velocity.GetHashCode();
        return hashCode;
    }
};

//[Serializable]
//public class Ship
//{
//    public Vector2 position;
//    public Vector2 velocity;
//    public float radius;
//    public float heading;
//    public int health;
//    public int cooldown;
//    public int score;
//    public Bullet[] bullets = new Bullet[MAX_BULLETS];

//    public void Serialize(BinaryWriter bw)
//    {
//        bw.Write(position.x);
//        bw.Write(position.y);
//        bw.Write(velocity.x);
//        bw.Write(velocity.y);
//        bw.Write(radius);
//        bw.Write(heading);
//        bw.Write(health);
//        bw.Write(cooldown);
//        bw.Write(score);
//        for (int i = 0; i < MAX_BULLETS; ++i)
//        {
//            bullets[i].Serialize(bw);
//        }
//    }

//    public void Deserialize(BinaryReader br)
//    {
//        position.x = br.ReadSingle();
//        position.y = br.ReadSingle();
//        velocity.x = br.ReadSingle();
//        velocity.y = br.ReadSingle();
//        radius = br.ReadSingle();
//        heading = br.ReadSingle();
//        health = br.ReadInt32();
//        cooldown = br.ReadInt32();
//        score = br.ReadInt32();
//        for (int i = 0; i < MAX_BULLETS; ++i)
//        {
//            bullets[i].Deserialize(br);
//        }
//    }

//    // @LOOK Not hashing bullets.
//    public override int GetHashCode()
//    {
//        int hashCode = 1858597544;
//        hashCode = hashCode * -1521134295 + position.GetHashCode();
//        hashCode = hashCode * -1521134295 + velocity.GetHashCode();
//        hashCode = hashCode * -1521134295 + radius.GetHashCode();
//        hashCode = hashCode * -1521134295 + heading.GetHashCode();
//        hashCode = hashCode * -1521134295 + health.GetHashCode();
//        hashCode = hashCode * -1521134295 + cooldown.GetHashCode();
//        hashCode = hashCode * -1521134295 + score.GetHashCode();
//        return hashCode;
//    }
//}

[Serializable]
public struct GGPOGameState : IGame
{
    public const int INPUT_FORWARD = (1 << 0);
    public const int INPUT_BACKWARD = (1 << 1);
    public const int INPUT_LEFT = (1 << 2);
    public const int INPUT_RIGHT = (1 << 3);

    public long UnserializedInputsP1 { get; private set; }
    public long UnserializedInputsP2 { get; private set; }

    public int Framenumber { get; private set; }
    public int Checksum => GetHashCode();

    public Player[] Players;

    private int _Speed;

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(Framenumber);
        writer.Write(Players.Length);

        for (int i = 0; i < Players.Length; ++i)
        {
            Players[i].Serialize(writer);
        }
    }

    public void Deserialize(BinaryReader reader)
    {
        Framenumber = reader.ReadInt32();
        int length = reader.ReadInt32();

        if (length != Players.Length)
        {
            Players = new Player[length];
        }

        for (int i = 0; i < Players.Length; ++i)
        {
            Players[i].Deserialize(reader);
        }
    }

    public NativeArray<byte> ToBytes()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                Serialize(writer);
            }

            return new NativeArray<byte>(memoryStream.ToArray(), Allocator.Persistent);
        }
    }

    public void FromBytes(NativeArray<byte> bytes)
    {
        using (var memoryStream = new MemoryStream(bytes.ToArray()))
        {
            using (var reader = new BinaryReader(memoryStream))
            {
                Deserialize(reader);
            }
        }
    }


    /*
        * InitGameState --
        *
        * Initialize our game state.
        */

    public GGPOGameState(int num_players)
    {
        // consts
        _Speed = 1;

        Framenumber = 0;
        UnserializedInputsP1 = 0;
        UnserializedInputsP2 = 0;

        Players = new Player[num_players];

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player();
        }
    }

    public void InitPlayer(int index, Vector3 startPosition)
    {
        Players[index].position = startPosition;
    }

    public Player GetPlayer(int index)
    {
        return Players[index];
    }

    public void ParsePlayerInputs(long inputs, int i)
    {
        GGPORunner.LogGame($"parsing ship {i} inputs: {inputs}.");
        
        Players[i].velocity.Set(0, 0, 0);

        if ((inputs & INPUT_LEFT) != 0)
        {
            Players[i].velocity.Set(-1, 0, 0);
        }

        if ((inputs & INPUT_RIGHT) != 0)
        {
            Players[i].velocity.Set(1, 0, 0);
        }

        if ((inputs & INPUT_FORWARD) != 0)
        {
            Players[i].velocity.Set(0, 0, 1);
        }

        if ((inputs & INPUT_BACKWARD) != 0)
        {
            Players[i].velocity.Set(0, 0, -1);
        }

        Players[i].velocity = Players[i].velocity.normalized * _Speed;
    }

    public void MovePlayer(int index)
    {
        var player = Players[index];

        Players[index].position = player.position + player.velocity;
    }

    public void MoveShip(int index, float heading, float thrust, int fire)
    {
        //var ship = _ships[index];

        //GGPORunner.LogGame($"calculation of new ship coordinates: (thrust:{thrust} heading:{heading}).");

        //ship.heading = heading;

        //if (ship.cooldown == 0)
        //{
        //    if (fire != 0)
        //    {
        //        GGPORunner.LogGame("firing bullet.");
        //        for (int i = 0; i < ship.bullets.Length; i++)
        //        {
        //            float dx = Mathf.Cos(DegToRad(ship.heading));
        //            float dy = Mathf.Sin(DegToRad(ship.heading));
        //            if (!ship.bullets[i].active)
        //            {
        //                ship.bullets[i].active = true;
        //                ship.bullets[i].position.x = ship.position.x + (ship.radius * dx);
        //                ship.bullets[i].position.y = ship.position.y + (ship.radius * dy);
        //                ship.bullets[i].velocity.x = ship.velocity.x + (BULLET_SPEED * dx);
        //                ship.bullets[i].velocity.y = ship.velocity.y + (BULLET_SPEED * dy);
        //                ship.cooldown = BULLET_COOLDOWN;
        //                break;
        //            }
        //        }
        //    }
        //}

        //if (thrust != 0)
        //{
        //    float dx = thrust * Mathf.Cos(DegToRad(heading));
        //    float dy = thrust * Mathf.Sin(DegToRad(heading));

        //    ship.velocity.x += dx;
        //    ship.velocity.y += dy;
        //    float mag = Mathf.Sqrt(ship.velocity.x * ship.velocity.x +
        //                     ship.velocity.y * ship.velocity.y);
        //    if (mag > SHIP_MAX_THRUST)
        //    {
        //        ship.velocity.x = (ship.velocity.x * SHIP_MAX_THRUST) / mag;
        //        ship.velocity.y = (ship.velocity.y * SHIP_MAX_THRUST) / mag;
        //    }
        //}
        //GGPORunner.LogGame($"new ship velocity: (dx:{ship.velocity.x} dy:{ship.velocity.y}).");

        //ship.position.x += ship.velocity.x;
        //ship.position.y += ship.velocity.y;
        //GGPORunner.LogGame($"new ship position: (dx:{ship.position.x} dy:{ship.position.y}).");

        //if (ship.position.x - ship.radius < _bounds.xMin ||
        //    ship.position.x + ship.radius > _bounds.xMax)
        //{
        //    ship.velocity.x *= -1;
        //    ship.position.x += (ship.velocity.x * 2);
        //}
        //if (ship.position.y - ship.radius < _bounds.yMin ||
        //    ship.position.y + ship.radius > _bounds.yMax)
        //{
        //    ship.velocity.y *= -1;
        //    ship.position.y += (ship.velocity.y * 2);
        //}
        //for (int i = 0; i < ship.bullets.Length; i++)
        //{
        //    if (ship.bullets[i].active)
        //    {
        //        ship.bullets[i].position.x += ship.bullets[i].velocity.x;
        //        ship.bullets[i].position.y += ship.bullets[i].velocity.y;
        //        if (ship.bullets[i].position.x < _bounds.xMin ||
        //            ship.bullets[i].position.y < _bounds.yMin ||
        //            ship.bullets[i].position.x > _bounds.xMax ||
        //            ship.bullets[i].position.y > _bounds.yMax)
        //        {
        //            ship.bullets[i].active = false;
        //        }
        //        else
        //        {
        //            for (int j = 0; j < _ships.Length; j++)
        //            {
        //                var other = _ships[j];
        //                if (Distance(ship.bullets[i].position, other.position) < other.radius)
        //                {
        //                    ship.score++;
        //                    other.health -= BULLET_DAMAGE;
        //                    ship.bullets[i].active = false;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public void LogInfo(string filename)
    {
        //string fp = "";
        //fp += "GameState object.\n";
        //fp += string.Format("  bounds: {0},{1} x {2},{3}.\n", _bounds.xMin, _bounds.yMin, _bounds.xMax, _bounds.yMax);
        //fp += string.Format("  num_ships: {0}.\n", _ships.Length);
        //for (int i = 0; i < _ships.Length; i++)
        //{
        //    var ship = _ships[i];
        //    fp += string.Format("  ship {0} position:  %.4f, %.4f\n", i, ship.position.x, ship.position.y);
        //    fp += string.Format("  ship {0} velocity:  %.4f, %.4f\n", i, ship.velocity.x, ship.velocity.y);
        //    fp += string.Format("  ship {0} radius:    %d.\n", i, ship.radius);
        //    fp += string.Format("  ship {0} heading:   %d.\n", i, ship.heading);
        //    fp += string.Format("  ship {0} health:    %d.\n", i, ship.health);
        //    fp += string.Format("  ship {0} cooldown:  %d.\n", i, ship.cooldown);
        //    fp += string.Format("  ship {0} score:     {1}.\n", i, ship.score);
        //    for (int j = 0; j < ship.bullets.Length; j++)
        //    {
        //        fp += string.Format("  ship {0} bullet {1}: {2} {3} -> {4} {5}.\n", i, j,
        //                ship.bullets[j].position.x, ship.bullets[j].position.y,
        //                ship.bullets[j].velocity.x, ship.bullets[j].velocity.y);
        //    }
        //}
        //File.WriteAllText(filename, fp);
    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        Framenumber++;

        // hacky way to store inputs on state
        UnserializedInputsP1 = inputs[0];
        UnserializedInputsP2 = inputs[1];

        for (int i = 0; i < Players.Length; i++)
        {
            ParsePlayerInputs(inputs[i], i);
            MovePlayer(i);
        }    

        //for (int i = 0; i < _ships.Length; i++)
        //{
        //    float thrust, heading;
        //    int fire;

            //    if ((disconnect_flags & (1 << i)) != 0)
            //    {
            //        GetShipAI(i, out heading, out thrust, out fire);
            //    }
            //    else
            //    {
            //        ParseShipInputs(inputs[i], i, out heading, out thrust, out fire);
            //    }
            //    MoveShip(i, heading, thrust, fire);

            //    if (_ships[i].cooldown != 0)
            //    {
            //        _ships[i].cooldown--;
            //    }
            //}
    }

    public long ReadInputs(int id)
    {
        long input = 0;

        if (Input.GetKey(KeyCode.W))
        {
            input |= INPUT_FORWARD;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input |= INPUT_BACKWARD;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input |= INPUT_LEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input |= INPUT_RIGHT;
        }

        return input;
    }

    public void FreeBytes(NativeArray<byte> data)
    {
        if (data.IsCreated)
        {
            data.Dispose();
        }
    }

    public override int GetHashCode()
    {
        int hashCode = -1214587014;
        hashCode = hashCode * -1521134295 + Framenumber.GetHashCode();
        foreach (var player in Players)
        {
            hashCode = hashCode * -1521134295 + player.GetHashCode();
        }
        return hashCode;
    }
}