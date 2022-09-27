import { ISettings } from './Home';
import React, { useRef, useEffect } from 'react'


const Animation = (props:ISettings) => {

	const canvasRef = useRef<HTMLCanvasElement>(null);
	var parts = props.Description.split(' ');
	if (parts.length < 4) {
		const len = parts.length;
		for (let i = len; i < 4; i++) {
			parts.push('');
		}
	} else if (parts.length > 4) {
		for (let i = 4; i < parts.length; i++) {
			parts[3] += parts[i];
        }
    }

	const draw = (ctx:any, frameCount:number) => {
		
		let grd = ctx.createLinearGradient(0, 0, 200, 0);
		grd.addColorStop(0, "lightsteelblue");
		grd.addColorStop(.5, "cornflowerblue");
		grd.addColorStop(1, "lightsteelblue");

		ctx.fillStyle = grd;
		
		if (frameCount == 20) {
			ctx.font = "30px Arial";
			ctx.fillText(props.Name, 10, 50);
		}
		if (frameCount == 35) {
			ctx.lineCap = "round";
			ctx.strokeStyle = '#eeeeee';
			ctx.moveTo(10, 65);
			ctx.lineTo(250, 65);
			ctx.stroke();
		}
		if (frameCount == 50) {
			ctx.font = "20px Arial";

			ctx.fillStyle = grd;
			ctx.fillText(parts[0], 10, 100);

		}
		if (frameCount == 75) {

			ctx.fillStyle = grd;
			ctx.fillText(parts[1], 50, 100);

		}


		if (frameCount == 100) {

			ctx.fillText(parts[2],105, 100);

		}
		if (frameCount == 125) {

			ctx.fillText(parts[3], 150, 100);

		}


		if (frameCount == 200) {
			ctx.font = "12px Arial";
			ctx.fillText(props.PhoneNumber, 10, 125);
        }

		if (frameCount >= 300) {
			return;
        }

    };

	useEffect(() => {
		let canvas = canvasRef.current;
		if (canvas === null) {
			return window.cancelAnimationFrame(0);
        }
		const context = canvas.getContext('2d');
		let frameCount = 0;
		let animationFrameId:number;

		const render = () => {
			frameCount++;
			draw(context, frameCount);
			animationFrameId = window.requestAnimationFrame(render);
		};
		render();

		return () => {
			window.cancelAnimationFrame(animationFrameId);
		};
	}, []);

    return <canvas style={{ width: '700px'  }} ref={canvasRef} />;
}

export default Animation