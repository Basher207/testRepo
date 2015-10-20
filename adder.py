import time
import BaseHTTPServer

class MyHandler(BaseHTTPServer.BaseHTTPRequestHandler):
  def do_GET(s):
    """Respond to a GET request."""
    s.send_response(200)
    s.send_header("Content-type", "text/html")
    s.end_headers()
    s.wfile.write("The path was %s" % s.path)


httpd = BaseHTTPServer.HTTPServer(("0.0.0.0", 8080), MyHandler)
httpd.serve_forever()