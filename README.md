# Scripts/Data 
Scripts refers to the scripts used to generate the photos and csv data for model generation. This was done in Unity using a combination of feet, ID, and background textures. Additionally Unity was needed to create environments in which the lighting, orientation, background, color, and size of the objects could be easily controlled.

# Model Generation
The model was generated as a TFLite Model using the object detection model maker jupyter notebook from Tensor Flow https://github.com/tensorflow/tensorflow/blob/master/tensorflow/lite/g3doc/models/modify/model_maker/object_detection.ipynb

# Model
Model.tflite is the model generated from our data which can detect TAMU_ID cards and either Feet when vertically orientated

# References
The example Android app provided by TensorFlow was utilized in integrating the model with the other subsystems in Kotlin. Both ObjectDetectorHelper and CameraFragment were edited and used in the final build. https://github.com/tensorflow/examples/tree/master/lite/examples/object_detection/android 

Copyright 2022 The TensorFlow Authors. All Rights Reserved.
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

# Size Measurement Error Testing
Measured:	Results:

26.9:	    27.2 27.1 25.9

27.1:	     26.7 27.2 26.8

24.5:    24.6 23.7 24.1

24.3:     24.4 24.2 23.9

27.1:	    27.2 26.7 26.4 

26.6: 	    26.9 28.3 27.6 

# 5.19 Percent Error
0.051989 error: approximately 1 cm of error

Improvement from 403 model with 3cm of error

# Model Creation
~7000 images; 640 x 360

batch_size=8, train_whole_model=True,epochs=50

Model architecture:EfficientDet-Lite0 	

Size(MB): 4.4	

Latency(ms): 37

Average Precision:25.69%

Original Model
{'AP': 0.90574837,
 'AP50': 0.9685112,
 'AP75': 0.94431263,
 'APs': 0.5490857,
 'APm': 0.9135043,
 'APl': 0.9519093,
 'ARmax1': 0.92758334,
 'ARmax10': 0.943,
 'ARmax100': 0.9435,
 'ARs': 0.6533333,
 'ARm': 0.9516123,
 'ARl': 0.9774908,
 'AP_/FOOT': 0.95422834,
 'AP_/TAMU_ID': 0.8572684}
 
 TFLite Conversion
{'AP': 0.9120786,
 'AP50': 0.97039604,
 'AP75': 0.96059406,
 'APs': -1.0,
 'APm': 0.91063535,
 'APl': 0.9450263,
 'ARmax1': 0.94666666,
 'ARmax10': 0.94666666,
 'ARmax100': 0.94666666,
 'ARs': -1.0,
 'ARm': 0.94343275,
 'ARl': 0.965582,
 'AP_/TAMU_ID': 0.8857431,
 'AP_/FOOT': 0.9384141}




