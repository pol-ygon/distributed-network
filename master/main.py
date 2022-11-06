from flask import Flask, jsonify, request
import mysql.connector, hashlib
from datetime import datetime, timedelta

app = Flask(__name__)

def update_online_status(tk):
    dt = (datetime.now()).strftime("%Y-%m-%d %H:%M:%S")

    mydb = mysql.connector.connect(
        host="localhost",
        user="paul",
        password="paul2022!",
        database="DISTRIBUITED_NETWORK"
    )
    mycursor = mydb.cursor()
    sql = "UPDATE t_nodes SET last_com = %s WHERE sn = %s"

    mycursor.execute(sql, (dt, tk))
    mydb.commit()

@app.route('/online_nodes')
def online_nodes():
    response = []
    token = request.args.get('token')
    bios = hashlib.sha256((request.args.get('bios')).encode()).hexdigest()
    mac = hashlib.sha256((request.args.get('mac')).encode()).hexdigest()
    sn = hashlib.sha256((bios+mac).encode()).hexdigest()

    if(sn == token):
        mydb = mysql.connector.connect(
            host="localhost",
            user="paul",
            password="paul2022!",
            database="DISTRIBUITED_NETWORK"
        )

        mycursor = mydb.cursor()

        sql = "SELECT * FROM t_nodes WHERE bios = %s AND mac = %s AND sn = %s"
        mycursor.execute(sql, (bios,mac,sn))

        nodes_online = 0
        myresult = mycursor.fetchall()
        if(len(myresult) > 0):
            dt_start = (datetime.now() - timedelta(minutes=2)).strftime("%Y-%m-%d %H:%M:%S") 
            dt_stop  = (datetime.now()).strftime("%Y-%m-%d %H:%M:%S")

            sql = "SELECT * FROM t_nodes WHERE last_com BETWEEN %s AND %s"
            mycursor.execute(sql, (dt_start,dt_stop))
            nodes_online = len(mycursor.fetchall())
        return jsonify([{"status": 200, "nodes": nodes_online}])
    return jsonify([{"status": 400, "nodes": -1}])

@app.route('/jobs')
def jobs():
    jobs = []

    token = request.args.get('token')
    bios = hashlib.sha256((request.args.get('bios')).encode()).hexdigest()
    mac = hashlib.sha256((request.args.get('mac')).encode()).hexdigest()
    sn = hashlib.sha256((bios+mac).encode()).hexdigest()

    if(sn == token):
        mydb = mysql.connector.connect(
            host="localhost",
            user="paul",
            password="paul2022!",
            database="DISTRIBUITED_NETWORK"
        )

        mycursor = mydb.cursor()

        sql = "SELECT * FROM t_nodes WHERE bios = %s AND mac = %s AND sn = %s"
        mycursor.execute(sql, (bios,mac,sn))

        myresult = mycursor.fetchall()
        if(len(myresult) > 0):
            id_node = str(myresult[0][0])
            jobs.append({"status": 200})
            #for i in range(0,20):
            # Check DB T_JOBS to do
            sql = "SELECT * FROM t_jobs WHERE id_node = %s AND dt_finish is NULL"
            mycursor.execute(sql, (id_node,))
            myresult = mycursor.fetchall()

            print(len(myresult))
            count = 1
            for res in myresult:
                id_job  = res[0]
                desc    = res[2]
                type    = res[3]
                script  = hashlib.sha256((res[4]).encode()).hexdigest()
                lang    = res[5]
                dt_reg  = res[6]
                weight  = res[9]
                n_nodes = res[10]
                serial  = hashlib.sha256((str(id_job) + " " + desc + " " + type + " " + script + " " + lang + " " + str(dt_reg)).encode()).hexdigest()

                jobs.append({"local_id": str(count), "global_id": id_job, "desc": desc, "serial": serial,"type": type, "source_script": script, "lang": lang})
                count += 1

            update_online_status(token)
            return jsonify(jobs)
        
    jobs = []
    return jsonify({"status": 400})

@app.route('/register_node', methods = ['GET'])
def register_node():
    #sn = request.args.get('sn')
    bios = hashlib.sha256((request.args.get('bios')).encode()).hexdigest()
    mac = hashlib.sha256((request.args.get('mac')).encode()).hexdigest()

    mydb = mysql.connector.connect(
        host="localhost",
        user="paul",
        password="paul2022!",
        database="DISTRIBUITED_NETWORK"
    )

    mycursor = mydb.cursor()

    sql = "SELECT * FROM t_nodes WHERE bios = %s AND mac = %s"
    mycursor.execute(sql, (bios,mac))

    myresult = mycursor.fetchall()

    if(len(myresult) == 0):
        sn = hashlib.sha256((bios+mac).encode()).hexdigest()
        dt = (datetime.now()).strftime("%Y-%m-%d %H:%M:%S")

        sql = "INSERT INTO t_nodes (bios, mac, sn, jobs_done, last_com) VALUES (%s, %s, %s, %s, %s)"
        mycursor.execute(sql, (bios,mac,sn,0,dt))
        mydb.commit()

        response = [{
            "message": "registered", "status": "200", "token": sn
        }]

        return jsonify(response)

    update_online_status(myresult[0][4])
    response = [{
        "message": "already registered", "status": "201", "token": myresult[0][4]
    }]

    return jsonify(response)

@app.route("/scripts", methods = ['GET'])
def get_script():
    file = request.args.get('file')
    token = request.args.get('token')
    bios = hashlib.sha256((request.args.get('bios')).encode()).hexdigest()
    mac = hashlib.sha256((request.args.get('mac')).encode()).hexdigest()
    sn = hashlib.sha256((bios+mac).encode()).hexdigest()

    if(token == sn):
        mydb = mysql.connector.connect(
            host="localhost",
            user="paul",
            password="paul2022!",
            database="DISTRIBUITED_NETWORK"
        )

        mycursor = mydb.cursor()

        sql = "SELECT * FROM t_nodes WHERE bios = %s AND mac = %s AND sn = %s"
        mycursor.execute(sql, (bios,mac,sn))

        myresult = mycursor.fetchall()
        if(len(myresult) > 0):
            

    return send_file(f"./scripts/{file_name}")

if __name__ == '__main__':
    app.run()