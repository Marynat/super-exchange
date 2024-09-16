import { useEffect, useState } from 'react';
import './App.css';

interface Currency {
	name: string;
	code: string;
	mid: number;
	ask: number;
	bid: number;
}

function App() {
	const [currencies, setCurrencies] = useState<Currency[]>();

	useEffect(() => {
		populateCurrency();
	}, []);

	const contents = currencies === undefined
		? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
		: <table className="table table-striped" aria-labelledby="tableLabel">
			<thead>
				<tr>
					<th>Name</th>
					<th>Code</th>
					<th>Price Mid</th>
					<th>Price Bid</th>
					<th>Price Ask</th>
				</tr>
			</thead>
			<tbody>
				{currencies.map(currency =>
					<tr key={currency.code}>
						<td>{currency.name}</td>
						<td>{currency.code}</td>
						<td>{currency.mid}</td>
						<td>{currency.bid}</td>
						<td>{currency.ask}</td>
					</tr>
				)}
			</tbody>
		</table>;

	return (
		<div>
			<h1 id="tableLabel">Todays Currencies</h1>
			<p>This component demonstrates fetching data from the server.</p>
			{contents}
		</div>
	);

	async function populateCurrency() {
		const response = await fetch('currency');
		const data = await response.json();
		setCurrencies(data);
	}
}

export default App;