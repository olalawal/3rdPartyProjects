import streamlit as st
from datetime import datetime

# --- Calculation Functions ---
def calculate_short_pnl(entry_price_sol_fart, current_price_sol_fart, amount_fart_shorted):
    """
    Calculates the P&L of a short perpetual position in SOL.
    """
    pnl_sol = (entry_price_sol_fart - current_price_sol_fart) * amount_fart_shorted
    return pnl_sol

# --- Streamlit Application ---
st.set_page_config(layout="wide")
st.title("FART/SOL Leveraged Short Position Calculator")

st.write(f"*(As of {datetime.now().strftime('%Y-%m-%d %H:%M:%S CDT')})*")
st.write("This tool calculates the P&L for a pure leveraged short position on FART/SOL and helps determine a stop-loss price to protect your capital.")
st.warning("Disclaimer: This is a simplified model for educational purposes only. It does not account for trading fees, funding rates, slippage, liquidation risks, or other real-world complexities. **Do not use for actual trading decisions.** FART is a highly volatile asset; exercise extreme caution. Leverage amplifies both gains and losses.")

# --- Input Section ---
st.header("1. Input Your Position Details & Current Prices")

col1, col2 = st.columns(2)
with col1:
    initial_capital_usd = st.number_input(
        "Your Initial Capital for Short Position (in USD)",
        min_value=10.0,
        value=500.0,
        step=10.0,
        format="%.2f",
        help="The amount of your own capital (margin) you put into the leveraged short position, denominated in USD."
    )
    current_sol_price_usd = st.number_input(
        "Current Price of SOL (in USD)",
        min_value=0.01,
        value=147.00, # Example current SOL price
        step=0.10,
        format="%.2f",
        help="The current market price of 1 SOL token in USD."
    )
    leverage_factor = st.number_input(
        "Leverage Factor (e.g., 5 for 5x)",
        min_value=1.0,
        max_value=100.0, # Most exchanges offer up to 100x
        value=5.0,
        step=0.5,
        help="The leverage applied to your short position. Your notional position size will be Initial Capital x Leverage."
    )

with col2:
    current_fart_price_usd = st.number_input(
        "Current Price of FART (in USD)",
        min_value=0.000000001, # FART can be extremely low
        value=0.00000015, # Example current FART price in USD (from search results)
        step=0.000000001,
        format="%.10f", # Using more decimals for FART
        help="The current market price of 1 FART token in USD."
    )
    st.markdown("<br>", unsafe_allow_html=True) # Add some space
    st.subheader("Simulate FART Price Movement")
    price_change_percent = st.slider(
        "Simulated FART Price Change (%)",
        min_value=-90, # FART can have massive drops
        max_value=90,  # Or massive pumps
        value=-6, # Default to a 6% drop as per your request
        step=1,
        format="%d%%",
        help="Simulate a percentage change in FART's price relative to SOL (or USD, as the ratio moves)."
    )
    st.subheader("Stop Loss Settings")
    max_risk_percent = st.slider(
        "Max Percentage of Initial Capital to Risk (%)",
        min_value=1,
        max_value=99,
        value=15, # Default to risking 15% of initial capital
        step=1,
        format="%d%%",
        help="The maximum percentage of your initial USD capital you are willing to lose on this trade before the stop loss is triggered."
    )


st.markdown("---")

# --- Derived Values & Calculations ---
if current_sol_price_usd <= 0:
    st.error("Current SOL price must be greater than zero.")
elif current_fart_price_usd <= 0:
    st.error("Current FART price must be greater than zero.")
else:
    # 1. Calculate Initial FART Price in SOL/FART ratio
    entry_fart_price_sol = current_fart_price_usd / current_sol_price_usd
    st.info(f"**Calculated Entry FART Price:** **{entry_fart_price_sol:,.8f} SOL per FART**")

    # 2. Convert Initial Capital to SOL
    initial_capital_sol = initial_capital_usd / current_sol_price_usd
    st.info(f"**Your Initial Capital (Margin):** **{initial_capital_sol:,.2f} SOL**")

    # 3. Calculate Notional Position Size
    notional_value_sol = initial_capital_sol * leverage_factor
    st.info(f"**Notional Short Position Size:** **{notional_value_sol:,.2f} SOL equivalent**")

    # 4. Calculate Amount of FART Shorted
    # Handle division by zero if FART price is extremely small and causes issues
    if entry_fart_price_sol == 0:
        st.error("Calculated FART price in SOL is zero, cannot determine shorted amount.")
        st.stop()
    amount_fart_shorted = notional_value_sol / entry_fart_price_sol
    st.info(f"**Amount of FART Shorted:** **{amount_fart_shorted:,.0f} FART** (approximately)") # Rounded for display

    st.markdown("---")
    st.header("2. Simulated Trade Outcome")

    # 5. Calculate Simulated New FART Price (in SOL/FART)
    simulated_new_fart_price_sol = entry_fart_price_sol * (1 + price_change_percent / 100)

    if simulated_new_fart_price_sol <= 0:
        st.error("Simulated new FART price (in SOL) is zero or negative. Please adjust initial FART price or price change.")
    else:
        st.markdown(f"**Simulated New FART Price:** **{simulated_new_fart_price_sol:,.8f} SOL per FART** ({price_change_percent:+.0f}% change)")

        # 6. Calculate P&L
        pnl_sol = calculate_short_pnl(entry_fart_price_sol, simulated_new_fart_price_sol, amount_fart_shorted)
        pnl_usd = pnl_sol * current_sol_price_usd

        st.subheader("Results:")
        st.write(f"- P&L (in SOL): **{pnl_sol:,.4f} SOL**")
        
        # Display P&L in USD and color code
        if pnl_usd >= 0:
            st.markdown(f"- P&L (in USD): <span style='color:green; font-weight:bold;'>${pnl_usd:,.2f}</span>", unsafe_allow_html=True)
        else:
            st.markdown(f"- P&L (in USD): <span style='color:red; font-weight:bold;'>${pnl_usd:,.2f}</span>", unsafe_allow_html=True)

        # --- Stop Loss Calculation ---
        st.markdown("---")
        st.header("3. Stop Loss Recommendation")

        max_risk_usd = initial_capital_usd * (max_risk_percent / 100)
        max_risk_sol = max_risk_usd / current_sol_price_usd

        if amount_fart_shorted == 0:
            st.warning("Cannot calculate stop loss: Amount of FART shorted is zero.")
        else:
            # Loss per FART unit (in SOL) to hit max risk
            loss_per_fart_unit_sol = max_risk_sol / amount_fart_shorted

            # Stop loss price is higher than entry price for a short
            stop_loss_price_sol_fart = entry_fart_price_sol + loss_per_fart_unit_sol

            st.write(f"You are willing to risk up to **{max_risk_percent}%** of your initial capital, which is **${max_risk_usd:,.2f} USD** (or **{max_risk_sol:,.4f} SOL**).")
            st.write(f"To limit your loss to this amount, you should set a **BUY STOP ORDER** at:")
            st.markdown(f"### **{stop_loss_price_sol_fart:,.8f} SOL per FART**")

            st.info(f"If the FART/SOL price reaches this level, your short position will be closed, resulting in a loss of approximately **{max_risk_sol:,.4f} SOL** (or **${max_risk_usd:,.2f} USD**).")
            st.warning(f"**Always place your stop loss immediately after opening your position.** This calculator does not account for slippage, which can cause your actual execution price to be worse than your stop price in volatile markets.")